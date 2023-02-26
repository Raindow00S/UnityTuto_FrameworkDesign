using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GamePanel : MonoBehaviour, IController
    {
        private ICountDownSystem mCountDownSystem;
        private IGameModel mGameModel;

        private void Start()
        {
            mCountDownSystem = this.GetSystem<ICountDownSystem>();
            mGameModel = this.GetModel<IGameModel>();

            // 第一次初始化
            mGameModel.Gold.RegisterOnValueChanged(OnGoldValueChange);
            mGameModel.Life.RegisterOnValueChanged(OnLifeValueChanged);
            mGameModel.Score.RegisterOnValueChanged(OnScoreValueChanged);
        }

        private void OnLifeValueChanged(int life)
        {
            transform.Find("TopRoot/LifeText").GetComponent<TextMeshProUGUI>().text = $"Life: {life}";
        }

        private void OnGoldValueChange(int gold)
        {
            transform.Find("TopRoot/GoldText").GetComponent<TextMeshProUGUI>().text = $"Gold: {gold}";
        }

        private void OnScoreValueChanged(int score)
        {
            transform.Find("TopRoot/ScoreText").GetComponent<TextMeshProUGUI>().text = $"Score: {score}";
        }

        private void Update()
        {
            // 每20帧更新一次
            if (Time.frameCount % 20 == 0)
            {
                transform.Find("TopRoot/CountDownText").GetComponent<TextMeshProUGUI>().text =
                    $"{mCountDownSystem.CrtRemainSeconds}s";
                mCountDownSystem.Update();
            }
        }

        private void OnDestroy()
        {
            mGameModel.Gold.UnRegisterOnValueChanged(OnGoldValueChange);
            mGameModel.Life.UnRegisterOnValueChanged(OnLifeValueChanged);
            mGameModel.Score.UnRegisterOnValueChanged(OnScoreValueChanged);
            mGameModel = null;
            mCountDownSystem = null;
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}