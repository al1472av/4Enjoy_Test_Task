using System;
using System.Collections.Generic;
using LifeGame.Services.Timer;
using UnityEngine;

namespace LifeGame.Services
{
    public static class ServiceLocator
    {
        private static Dictionary<string, IService> _services;

        public static void Clear()
        {
            _services = null;
        }

        public static void AddService(IService service)
        {
            _services ??= new Dictionary<string, IService>();
            _services.Add(service.GetType().Name, service);
        }

        public static T GetService<T>() where T : class, IService
        {
            if (_services.ContainsKey(typeof(T).Name))
                return (T)_services[typeof(T).Name];

            throw new NullReferenceException($"No service {typeof(T).Name}");
        }
    }
}