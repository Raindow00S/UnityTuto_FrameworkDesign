using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class UI : MonoBehaviour, IController
    {
        private void Start()
        {
            this.RegisterEvent<GamePassEvent>(OnGamePass);
            this.RegisterEvent<OnCountDownEndEvent>(OnGameOver).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnGamePass(GamePassEvent e)
        {
            transform.Find("Canvas/GamePanel").gameObject.SetActive(false);
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        }

        private void OnGameOver(OnCountDownEndEvent e)
        {
            transform.Find("Canvas/GamePanel").gameObject.SetActive(false);
            transform.Find("Canvas/GameOverPanel").gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<GamePassEvent>(OnGamePass);
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}