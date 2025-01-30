using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Config
{
    public class BaseMultiConfig<T, U> : ScriptableObject, IConfig where T : IConfigData where U : IConfig
    {
        public Type ConfigType => typeof(U);

        [SerializeField]
        public List<T> data;

        private Dictionary<string, T> dataDict = null;

        public Dictionary<string, T> Data
        {
            get
            {
                dataDict ??= data.ToDictionary(config => config.ID);
                return dataDict;
            }
        }
    }
}
