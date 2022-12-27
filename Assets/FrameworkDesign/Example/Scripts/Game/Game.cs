using System;
using FrameworkDesign.Example;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Game : MonoBehaviour, IController
    {
        private void Awake()
        {
            // GameStartEvent.Register(OnGameStart);
            this.RegisterEvent<GameStartEvent>(OnGameStart);
        }

        private void OnGameStart(GameStartEvent e)
        {
            transform.Find("Enemies").gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            // GameStartEvent.Unregister(OnGameStart);
            this.UnRegisterEvent<GameStartEvent>(OnGameStart);
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}