using System;
using UnityEngine;

namespace Core.Config
{
    public class BaseSingleConfig<T, U> : ScriptableObject, IConfig where T : IConfigData, new() where U : IConfig
    {
        public Type ConfigType => typeof(U);

        public T data;
    }
}

