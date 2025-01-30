using UnityEngine;
using UnityEngine.SceneManagement;

public class InitLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene(1);
    }
}
