using Core.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Services
{
    public class UserState : BaseService, IUser
    {
        public const string ExampleInteger = "example_int";
        public const string CustomState = "custom_State";
        public const string HighScore = "high_score";

        public int ExampleInt
        {
            get => GetValue(ExampleInteger, 0);
            set => SetValue<int>(ExampleInteger, value);
        }

        public int HighScoreValue
        {
            get => GetValue<int>(HighScore, 0);
            set => SetValue<int>(HighScore, value);
        }

        //public ExampleCustomState ExampleCustomState
        //{
        //    get { return GetValue(CustomState, new ExampleCustomState()); }
        //    set { SetValue<int>(CustomState, value); }
        //}

        private T GetValue<T>(string key, T defaultValue)
        {
            return PlayerPrefs.HasKey(key) ? JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key)) : defaultValue;
        }

        private void SetValue<T>(string key, object value)
        {
            var data = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            PlayerPrefs.SetString(key, data);
        }

        public void ClearUserState()
        {
            //ExampleCustomState.Reset();
            //StateHandler.OnClearAllStates?.Invoke();
        }
    }
}