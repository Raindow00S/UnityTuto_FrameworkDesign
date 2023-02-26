using System;
using TMPro;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class GamePassPanel : MonoBehaviour, IController
    {
        private void OnEnable()
        {
            // 没注册事件但需要每次显示前都刷新的值
            transform.Find("TopRoot/RemainTimeText").GetComponent<TextMeshProUGUI>().text =
                $"Remain Time: {this.GetSystem<ICountDownSystem>().CrtRemainSeconds}s";
            var gameModel = this.GetModel<IGameModel>();
            transform.Find("TopRoot/BestScoreText").GetComponent<TextMeshProUGUI>().text =
                $"Best Score: {gameModel.BestScore.Value}";
            transform.Find("TopRoot/ScoreText").GetComponent<TextMeshProUGUI>().text =
                $"Score: {gameModel.Score.Value}";
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}