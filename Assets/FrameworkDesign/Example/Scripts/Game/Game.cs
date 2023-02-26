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
            this.RegisterEvent<OnCountDownEndEvent>(OnGameOver).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<GamePassEvent>(OnGamePass).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnGameStart(GameStartEvent e)
        {
            var enemyRoot = transform.Find("Enemies");
            enemyRoot.gameObject.SetActive(true);
            foreach (Transform childTrans in enemyRoot)
            {
                childTrans.gameObject.SetActive(true);
            }
        }

        private void OnGameOver(OnCountDownEndEvent e)
        {
            transform.Find("Enemies").gameObject.SetActive(false);
        }
        
        private void OnGamePass(GamePassEvent e)
        {
            transform.Find("Enemies").gameObject.SetActive(false);
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