using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    // 接口、抽象类、实现类当中，接口方法显式实现的应用
    public class InterfaceStructExample : MonoBehaviour
    {
        public interface ICustomScript
        {
            void Start();
            void Update();
            void Destroy();
        }

        public abstract class CustomScript : ICustomScript
        {
            void ICustomScript.Start()
            {
                OnStart();
            }

            void ICustomScript.Update()
            {
                OnUpdate();
            }

            void ICustomScript.Destroy()
            {
                OnDestroy();
            }

            protected abstract void OnStart();
            protected abstract void OnUpdate();
            protected abstract void OnDestroy();
        }

        public class MyScript : CustomScript
        {
            protected override void OnStart()
            {
                // 会造成递归调用，所以改用显式实现，以增加调用难度
                // Start();
                Debug.Log("OnStart");
            }

            protected override void OnUpdate()
            {
                Debug.Log("OnUpdate");
            }

            protected override void OnDestroy()
            {
                Debug.Log("OnDestroy");
            }
        }

        private void Start()
        {
            // 现在必须要转换为接口才能调用Start等方法
            ICustomScript myScript = new MyScript();
            myScript.Start();
            myScript.Update();
            myScript.Destroy();
        }
    }
}