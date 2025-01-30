using Core.UI;
using UnityEngine;

namespace Dancestar.UI
{
    public class TestDialog : BaseDialog
    {
        public const string Dialog_ID = "TestDialog";

        [SerializeField] private UIButton closeButton;

        public void CustomInit()
        {
        }

        public override void OnDialogOpen()
        {
            UnityEngine.Debug.Log("TestDialog Init");

            base.OnDialogOpen();
            closeButton.AddPressedListener(CloseDialog);
        }

        public override void OnDialogClosed()
        {
            base.OnDialogClosed();
            closeButton.RemoveAllPressedListeners();
        }

        public void CloseOpenedDialog()
        {
            CloseDialog();
        }
    }
}
