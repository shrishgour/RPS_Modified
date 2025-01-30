using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Services
{
    public abstract class BaseService
    {
        public virtual void Initialize()
        {

        }
    }

    public static class ServiceRegistry
    {
        private static Dictionary<Type, BaseService> serviceMap;

        static ServiceRegistry()
        {
            serviceMap = new Dictionary<Type, BaseService>();
        }

        public static void Bind<T>(BaseService service)
        {
            try
            {
                serviceMap.Add(service.GetType(), service);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static void UnBind<T>()
        {
            serviceMap.Remove(typeof(T));
        }

        public static Z Get<Z>()
            where Z : BaseService
        {
            BaseService result = null;
            if (serviceMap.TryGetValue(typeof(Z), out result))
            {
                return result as Z;
            }

            return null;
        }

        public static void Clear()
        {
            serviceMap.Clear();
        }

    }
}

