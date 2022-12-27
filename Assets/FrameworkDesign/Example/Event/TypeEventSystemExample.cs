using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class TypeEventSystemExample : MonoBehaviour
    {
        public struct EventA
        {
            
        }

        public struct EventB
        {
            public int ParamB;
        }
        
        public interface IEventGroup
        {
            
        }
        
        public struct EventC : IEventGroup
        {
            
        }
        
        public struct EventD : IEventGroup
        {
            
        }

        private TypeEventSystem mTypeEventSystem = new TypeEventSystem();
        
        private void Start()
        {
            // 传统注册方式，需要手动在destroy时注销
            mTypeEventSystem.Register<EventA>(OnEventA);
            // 委托的注册方式，有参数，同时预备注销
            mTypeEventSystem.Register<EventB>(b =>
            {
                Debug.Log("OnEventB: " + b.ParamB);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            mTypeEventSystem.Register<IEventGroup>(e =>
            {
                Debug.Log(e.GetType());
                
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mTypeEventSystem.Send<EventA>();
            }

            if (Input.GetMouseButtonDown(1))
            {
                mTypeEventSystem.Send(new EventB()
                {
                    ParamB = 123
                });
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                mTypeEventSystem.Send<IEventGroup>(new EventC());
                mTypeEventSystem.Send<IEventGroup>(new EventD());
            }
        }

        private void OnEventA(EventA obj)
        {
            Debug.Log("OnEventA");
        }

        private void OnDestroy()
        {
            mTypeEventSystem.UnRegister<EventA>(OnEventA);
        }
    }   
}