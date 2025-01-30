using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SceneHandler
{
    public class BaseSceneHandler : MonoBehaviour
    {
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name);
            Debug.Log(mode);

            OnInitialize();
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        protected virtual void OnInitialize()
        {

        }
    }
}