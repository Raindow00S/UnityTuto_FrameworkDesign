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
            
            // new KillEnemyCommand().Execute();
            this.SendCommand<KillEnemyCommand>();
            Destroy(gameObject);
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PointGame.Interface;
        }
    }   
}
