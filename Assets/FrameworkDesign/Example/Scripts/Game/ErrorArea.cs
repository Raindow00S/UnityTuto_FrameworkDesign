using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class ErrorArea : MonoBehaviour, IController
    {
        private void OnMouseDown()
        {
            // Debug.Log("click error area");
            this.SendCommand<MissCommand>();
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}