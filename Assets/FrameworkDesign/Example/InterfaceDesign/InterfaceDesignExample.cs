using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface ICanSayHello
    {
        void SayHello();
        void SayOther();
    }
    
    
    public class InterfaceDesignExample : MonoBehaviour, ICanSayHello
    {
        // 接口的隐式实现
        public void SayHello()
        {
            Debug.Log("Hello");
        }

        // 接口的显式实现
        void ICanSayHello.SayOther()
        {
            Debug.Log("Other");
        }

        private void Start()
        {
            this.SayHello();
            // 接口的显式实现必须要转为接口才能调用
            (this as ICanSayHello).SayOther();
        }
    }
}