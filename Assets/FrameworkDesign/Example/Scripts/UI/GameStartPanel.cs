using System;
using FrameworkDesign;
using FrameworkDesign.Example;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GameStartPanel : MonoBehaviour, IController
    {
        // public GameObject enemies;   // *这里对敌人节点的直接引用，加入事件以后就不需要了

        private void Start()
        {
            Button btnStart = transform.Find("BtnGameStart").GetComponent<Button>();
            btnStart.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                this.SendCommand<StartGameCommand>();
                // new StartGameCommand().Execute();
            });
            
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}