using Core.UI;
using UnityEngine;

public class TestDialogRunner : MonoBehaviour
{
    [SerializeField] private UIButton openDialogBtn;
    // Start is called before the first frame update
    void Start()
    {

        openDialogBtn.AddPressedListener(OnBtnPress);
    }

    private void OnBtnPress()
    {
        UiManager.Instance.OpenDialog<ProgressionDialog>(ProgressionDialog.DialogID, false, null);
    }
}
