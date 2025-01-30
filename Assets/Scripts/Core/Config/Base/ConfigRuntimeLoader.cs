using Core.Config;
using UnityEngine;

public class ConfigRuntimeLoader : MonoBehaviour
{
    public ConfigRegistry registry;
    void Awake()
    {
        registry.Initilize();
    }
}
