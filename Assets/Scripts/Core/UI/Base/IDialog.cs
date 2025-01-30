using UnityEngine;

namespace Core.UI
{
    public interface IDialog
    {
        void OnDialogOpen();
        void OnDialogShown();

        void OnHide();
        void OnDialogClosed();
        void OnBackPressed();
        void OnClickBackground();
        void SetVisible(bool value);
        Transform transform { get; }
    }
}
