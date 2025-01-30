using System.Collections;
using UnityEngine;

namespace Core.UI
{
    public abstract class BaseDialog : MonoBehaviour, IDialog
    {
        public virtual int DialogQueuePriority() => 0;

        public virtual void OnDialogOpen()
        {

        }

        public IEnumerator Animate()
        {
            //Implement open tweening animations here
            yield return null;
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

        public void OnDialogShown()
        {
        }
    }
}