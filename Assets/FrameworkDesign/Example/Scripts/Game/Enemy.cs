using System.Collections;
using System.Collections.Generic;
using FrameworkDesign;
using FrameworkDesign.Example;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Enemy : MonoBehaviour, IController
    {
        public void OnMouseDown()
        {
            gameObject.SetActive(false);
            // new KillEnemyCommand().Execute();
            this.SendCommand<KillEnemyCommand>();
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PointGame.Interface;
        }
    }   
}
