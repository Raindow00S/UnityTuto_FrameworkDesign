using System;
using System.Collections.Generic;
using FrameworkDesign;
using UnityEngine;

namespace FrameworkDesign
{
    /// <summary>
    /// 可监听事件：Controller、System
    /// 可发送事件：Command、System、Model
    /// </summary>
    public interface ITypeEventSystem
    {
        void Send<T>() where T : new();
        void Send<T>(T e);
        IUnRegister Register<T>(Action<T> onEvent);
        void UnRegister<T>(Action<T> onEvent);
    }

    public interface IUnRegister
    {
        void UnRegister();
    }

    public struct TypeEventSystemUnRegister<T> : IUnRegister
    {
        public ITypeEventSystem typeEventSystem;
        public Action<T> OnEvent;
        
        public void UnRegister()
        {
            typeEventSystem.UnRegister<T>(OnEvent);
            typeEventSystem = null;
            OnEvent = null;
        }
    }

    // 物体销毁时自动注销事件
    public class UnRegisterOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnRegister> mUnRegisters = new HashSet<IUnRegister>();

        public void AddUnRegister(IUnRegister unRegister)
        {
            mUnRegisters.Add(unRegister);
        }

        private void OnDestroy()
        {
            foreach (var unRegister in mUnRegisters)
            {
                unRegister.UnRegister();
            }
            mUnRegisters.Clear();
        }
    }

    public static class UnRegisterExtension
    {
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();
            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }
            trigger.AddUnRegister(unRegister);
        }
    }
    
    
    public class TypeEventSystem : ITypeEventSystem
    {
        public interface IRegistrations
        {
        }

        public class Registrations<T> : IRegistrations
        {
            public Action<T> OnEvent = e => { };
        }

        private Dictionary<Type, IRegistrations> mEventRegistration = new Dictionary<Type, IRegistrations>();

        public void Send<T>() where T : new()
        {
            var e = new T();
            Send<T>(e);
        }

        public void Send<T>(T e)
        {
            var type = typeof(T);
            IRegistrations registrations;
            if (mEventRegistration.TryGetValue(type, out registrations))
            {
                (registrations as Registrations<T>).OnEvent(e);
            }
        }

        public IUnRegister Register<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            IRegistrations registrations;
            if (mEventRegistration.TryGetValue(type, out registrations))
            {
            }
            else
            {
                registrations = new Registrations<T>();
                mEventRegistration.Add(type,registrations);
            }

            (registrations as Registrations<T>).OnEvent += onEvent;
            return new TypeEventSystemUnRegister<T>()
            {
                OnEvent = onEvent,
                typeEventSystem = this
            };
        }

        public void UnRegister<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            IRegistrations registrations;
            if (mEventRegistration.TryGetValue(type, out registrations))
            {
                (registrations as Registrations<T>).OnEvent -= onEvent;
            }
        }
    }
}