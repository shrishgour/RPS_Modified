using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Core.Config
{
    [CreateAssetMenu(fileName = "ConfigRegistory", menuName = "Configs/ConfigRegistory", order = 1)]
    public class ConfigRegistry : ScriptableObject
    {
        public string configStoragePath = "Assets/ConfigAssets/";

        public List<ScriptableObject> configs = new List<ScriptableObject>();
        public static Dictionary<Type, IConfig> registry;

        public void Initilize()
        {
            registry = new Dictionary<Type, IConfig>();
            foreach (IConfig config in configs)
            {
                registry.Add(config.ConfigType, config);
            }
        }

        public static T GetConfig<T>() where T : IConfig
        {
            var configID = typeof(T);
            var result = registry.TryGetValue(configID, out IConfig config);

            if (!result)
            {
                Debug.Log($"Config of type {configID} is not registered in registory!!");
            }

            return (T)config;
        }

        [ContextMenu("Refresh")]
        public void RefreshConfig()
        {
#if UNITY_EDITOR
            var type = typeof(IConfig);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(p => type.IsAssignableFrom(p)
                && !typeof(BaseMultiConfig<,>).IsAssignableFrom(p)
                && !typeof(BaseSingleConfig<,>).IsAssignableFrom(p)
                && !typeof(NonConfig).IsAssignableFrom(p)
                && p.FullName != type.FullName).ToList();

            Dictionary<string, ScriptableObject> lookup = configs.ToDictionary(p => p.name, p => p);

            foreach (var item in types)
            {
                if (!lookup.ContainsKey(item.Name))
                {
                    string path = configStoragePath + item.Name + ".asset";
                    ScriptableObject asset = null;

                    if (System.IO.File.Exists(path))
                    {
                        asset = (ScriptableObject)UnityEditor.AssetDatabase.LoadAssetAtPath(path, type);
                    }
                    else
                    {
                        asset = CreateInstance(item);
                        IConfig config = asset as IConfig;
                        UnityEditor.AssetDatabase.CreateAsset(asset, path);
                    }

                    configs.Add(asset);
                }
            }
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        private void OnValidate()
        {
            if (configs.Count > 0)
            {
                foreach (var config in configs)
                {
                    if (config != null && config is not IConfig)
                    {
                        configs.Remove(config);
                        break;
                    }
                }
            }
        }
    }
}