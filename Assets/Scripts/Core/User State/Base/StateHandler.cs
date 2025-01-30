using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Core.Data
{
    public class StateHandler
    {
        public static Action OnClearAllStates;
        public virtual void Reset()
        {
            SetValue<int>(Key, this);
        }

        virtual public string Key { get; set; }

        private T GetValue<T>(string key, T defaultValue)
        {
            return PlayerPrefs.HasKey(key) ? JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key)) : defaultValue;
        }

        private void SetValue<T>(string key, object value)
        {
            PlayerPrefs.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
