using Core.Config;
using Core.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.UI
{
    public class UiManager : SerializedSingleton<UiManager>
    {
        public IDialog openedDialog = null;
        public Transform dilogRoot;

        public bool IsAnyDialogOpen => visibleDialogs.Count > 0 || dialogStack.Count > 0;
        private List<Tuple<string, IDialog>> dialogStack = new List<Tuple<string, IDialog>>();
        private Dictionary<string, GameObject> dialogPrefabMap = new Dictionary<string, GameObject>();
        private List<IDialog> visibleDialogs = new List<IDialog>();
        private IDialog TopOfDialogStack => dialogStack.Count > 0 ? dialogStack[dialogStack.Count - 1].Item2 : null;
        private List<IQueuedDialog> queuedDialogs = new List<IQueuedDialog>();

        public Action AllDialogsClosed;
        public Action<string> DialogOpened;
        public Action<string> DialogClosed;

        private interface IQueuedDialog
        {
            string Id { get; }
            int Priority { get; }
            void Dequeue(UiManager uiManager);
        }

        private class QueuedDialog<TDialog> : IQueuedDialog where TDialog : class, IDialog
        {
            public string dialogId;
            public int dialogPriority;
            public Action<TDialog> dialogInitializer;

            public string Id => dialogId;
            public int Priority => dialogPriority;

            public void Dequeue(UiManager uiManager)
            {
                uiManager.OpenDialog(Id, false, dialogInitializer);
            }
        }

        [Serializable]
        private class DialogData
        {
            public string dialogId;
            public GameObject dialog;
        }

        private void Start()
        {
            CreateOrLoadDialogs();
        }

        void CreateOrLoadDialogs()
        {
            var dialogs = ConfigRegistry.GetConfig<UIDialogConfig>().data;
            foreach (var item in dialogs)
            {
                GameObject go = Instantiate(item.prefab, dilogRoot);
                go.SetActive(false);
                dialogPrefabMap.Add(item.dialogID, go);
            }
        }

        public void OpenDialog<TDialog>(string uiDialogId, bool overlayOnPrevious, Action<TDialog> dialogInitializer)
            where TDialog : class, IDialog
        {
            if (dialogPrefabMap.ContainsKey(uiDialogId))
            {
                TDialog newDialog = null;
                if (dialogPrefabMap.TryGetValue(uiDialogId, out GameObject dialog))
                {
                    newDialog = InstantiateDialog<TDialog>(dialog);
                }
                dialogInitializer?.Invoke(newDialog);
                StartOpenDialog(uiDialogId, newDialog, overlayOnPrevious);
                DialogOpened?.Invoke(uiDialogId);
            }
        }

        public void QueueDialog<TDialog>(string uiDialogId, Action<TDialog> dialogInitializer)
            where TDialog : class, IDialog
        {
            if (dialogPrefabMap.ContainsKey(uiDialogId))
            {
                int queueIndex;
                for (queueIndex = 0; queueIndex < queuedDialogs.Count; queueIndex++)
                {
                    if (dialogPrefabMap.TryGetValue(uiDialogId, out GameObject gameObject))
                    {
                        if (gameObject.GetComponent<BaseDialog>().DialogQueuePriority() < queuedDialogs[queueIndex].Priority)
                        {
                            break;
                        }
                    }
                }

                queuedDialogs.Insert(queueIndex, new QueuedDialog<TDialog>
                {
                    dialogId = uiDialogId,
                    dialogPriority = queueIndex,
                    dialogInitializer = dialogInitializer
                });
            }
        }

        public bool IsDialogQueued<TDialog>(string dialogId) where TDialog : class, IDialog
        {
            foreach (QueuedDialog<TDialog> dialog in queuedDialogs)
            {
                if (dialog.dialogId == dialogId)
                {
                    return true;
                }
            }

            return false;
        }

        public TDialog GetDialog<TDialog>(string uiDialogId) where TDialog : class, IDialog
        {
            int positionInStack = dialogStack.FindLastIndex(tuple => tuple.Item1 == uiDialogId);
            return positionInStack > -1 ? dialogStack[positionInStack].Item2 as TDialog : null;
        }

        private TDialog InstantiateDialog<TDialog>(GameObject dialogPrefab) where TDialog : class, IDialog
        {
            TDialog dialogComponent = dialogPrefab.GetComponent<TDialog>();
            return dialogComponent;
        }

        private void StartOpenDialog(string uiDialogId, IDialog dialog, bool overlayOnPrevious)
        {
            IDialog previousDialog = TopOfDialogStack;
            if (previousDialog != null)
            {
                previousDialog.OnHide();

                if (!overlayOnPrevious)
                {
                    previousDialog.SetVisible(false);

                    if (visibleDialogs.Contains(previousDialog))
                    {
                        visibleDialogs.Remove(previousDialog);
                    }
                }
            }

            int stackIndex = dialogStack.FindLastIndex(tuple => tuple.Item2 == dialog);

            if (stackIndex < 0)
            {
                dialogStack.Add(Tuple.Create(uiDialogId, dialog));
            }

            if (TopOfDialogStack == dialog)
            {
                dialog.SetVisible(true);
                dialog.OnDialogOpen();
                dialog.transform.SetAsLastSibling();
            }
        }

        public void CloseDialog(string uiDialogId)
        {
            IDialog dialogInStack = GetDialog<IDialog>(uiDialogId);
            if (dialogInStack != null)
            {
                CloseDialog(dialogInStack);
            }
            else
            {
                queuedDialogs.RemoveAll(queuedDialog => queuedDialog.Id == uiDialogId);
            }
        }

        public void CloseDialog(IDialog dialog, bool immediate = false)
        {
            if (dialogStack.Count == 1)
            {
                AllDialogsClosed?.Invoke();
            }
            IDialog preTopOfStack = TopOfDialogStack;
            int positionInStack = dialogStack.FindIndex(tuple => tuple.Item2 == dialog);
            string dialogId = null;

            if (positionInStack > -1)
            {
                dialogId = dialogStack[positionInStack].Item1;
                dialogStack.RemoveAt(positionInStack);
                if (visibleDialogs.Contains(dialog))
                {
                    visibleDialogs.Remove(dialog);
                }
            }

            dialog.OnDialogClosed();
            DialogClosed?.Invoke(dialogId);

            if (dialog == preTopOfStack)
            {
                dialog.OnHide();

                IDialog nextTopOfStack = TopOfDialogStack;

                void DialogClosed()
                {
                    dialog.SetVisible(false);
                    if (nextTopOfStack != null && TopOfDialogStack == nextTopOfStack)
                    {
                        ShowDialog(nextTopOfStack);
                    }
                }
                DialogClosed();
            }
            else
            {
                dialog.SetVisible(false);
            }
        }

        private void ShowDialog(IDialog dialog)
        {
            dialog.SetVisible(true);
            dialog.OnDialogOpen();
            DialogOpened?.Invoke(dialogStack[dialogStack.Count - 1].Item1);

            bool wasVisible = visibleDialogs.Contains(dialog);
            if (!wasVisible)
            {
                visibleDialogs.Add(dialog);
            }
        }

        private void Update()
        {
            if (dialogStack.Count == 0 && queuedDialogs.Count > 0)
            {
                IQueuedDialog nextDialog = queuedDialogs[0];
                queuedDialogs.RemoveAt(0);
                nextDialog.Dequeue(this);
            }
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    UiManager.Instance.OpenDialog<RadialSliderDialog>(RadialSliderDialog.dialogId, false, null);
            //}
        }

        public string GetDialogID<TDialog>(GameObject dialog) where TDialog : class, IDialog
        {
            TDialog dialogComponent = dialog.GetComponent<TDialog>();
            var entry = dialogStack.FirstOrDefault(x => x.Item2 == dialogComponent);
            return entry != null ? entry.Item1 : string.Empty;
        }

        public void CloseAllDialogs()
        {
            CloseDialogStack();
            queuedDialogs.Clear();
        }

        public void CloseDialogStack()
        {
            for (int index = dialogStack.Count - 1; index >= 0; index--)
            {
                IDialog dialog = dialogStack[index].Item2;
                CloseDialog(dialog, true);
            }
        }
    }
}
