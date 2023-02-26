using System;
using FrameworkDesign;
using FrameworkDesign.Example;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GameStartPanel : MonoBehaviour, IController
    {
        private IGameModel mGameModel;
        private bool mInited;

        private void Start()
        {
            Button btnStart = transform.Find("BtnGameStart").GetComponent<Button>();
            btnStart.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                this.SendCommand<StartGameCommand>();
                // new StartGameCommand().Execute();
            });
            
            transform.Find("TopRoot/BtnBuyLife").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<BuyLifeCommand>();
            });

            mGameModel = this.GetModel<IGameModel>();
            mGameModel.Gold.RegisterOnValueChanged(OnGoldValueChanged);
            mGameModel.Life.RegisterOnValueChanged(OnLifeValueChanged);
            
            // 第一次手动初始化，OnEnable先于Start执行
            OnGoldValueChanged(mGameModel.Gold.Value);
            OnLifeValueChanged(mGameModel.Life.Value);

            transform.Find("TopRoot/BestScoreText").GetComponent<TextMeshProUGUI>().text =
                $"Best Score: {mGameModel.BestScore.Value}";

            mInited = true;
        }

        private void OnEnable()
        {
            if (!mInited) return;

            // 一些没注册事件，但需要每次显示前都刷新以下的值
            transform.Find("TopRoot/BestScoreText").GetComponent<TextMeshProUGUI>().text =
                $"Best Score: {mGameModel.BestScore.Value}";
        }

        void OnGoldValueChanged(int gold)
        {
            if(gold > 0)
                transform.Find("TopRoot/BtnBuyLife").gameObject.SetActive(true);
            else
                transform.Find("TopRoot/BtnBuyLife").gameObject.SetActive(false);
            transform.Find("TopRoot/GoldText").GetComponent<TextMeshProUGUI>().text = $"Gold: {gold}";
        }

        void OnLifeValueChanged(int life)
        {
            transform.Find("TopRoot/LifeText").GetComponent<TextMeshProUGUI>().text = $"Life: {life}";
        }

        private void OnDestroy()
        {
            mGameModel.Gold.UnRegisterOnValueChanged(OnGoldValueChanged);
            mGameModel.Life.UnRegisterOnValueChanged(OnLifeValueChanged);
            mGameModel = null;
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}