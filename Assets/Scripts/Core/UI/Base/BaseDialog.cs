using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Core.UI
{
    public abstract class BaseDialog : MonoBehaviour, IDialog
    {
        public virtual int DialogQueuePriority()
        {
            return 0;
        }

        public virtual void OnDialogOpen()
        {
            StartCoroutine(Animate());
        }

        public IEnumerator Animate()
        {
            transform.localScale = Vector3.one * 5;
            transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.Flash);
            yield return new WaitForSeconds(0.5f);
            OnDialogShown();
        }

        public virtual void OnDialogClosed()
        {
        }

        public virtual void OnHide()
        {
        }

        public virtual void OnBackPressed()
        {
        }

        public virtual void OnClickBackground()
        {
        }

        protected void CloseDialog()
        {
            UiManager.Instance.CloseDialog(this);
        }

        public void SetVisible(bool value)
        {
            gameObject.SetActive(value);
        }

        public virtual void OnDialogShown()
        {
        }
    }
}