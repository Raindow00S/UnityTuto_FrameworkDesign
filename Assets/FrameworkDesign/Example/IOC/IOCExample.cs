using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class IOCExample : MonoBehaviour
    {
        private void Start()
        {
            // 注册一个蓝牙manager到IOC容器中，并执行方法
            var container = new IOCContainer();
            container.Register<IBluetoothManager>(new BluetoothManager());

            var bluetoothManager = container.Get<IBluetoothManager>();
            bluetoothManager.Connect();
        }

        public interface IBluetoothManager
        {
            void Connect();
        }
        
        public class BluetoothManager : IBluetoothManager
        {
            public void Connect()
            {
                Debug.Log("蓝牙连接");
            }
        }
    }
}