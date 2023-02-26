using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface IScoreSystem : ISystem
    {
        
    }

    public class ScoreSystem : AbstractSystem, IScoreSystem
    {
        protected override void OnInit()
        {
            var gameModel = this.GetModel<IGameModel>();

            // 游戏结束时计算剩余时间的分数，计算最高分
            this.RegisterEvent<GamePassEvent>(e =>
            {
                var countDownSystem = this.GetSystem<ICountDownSystem>();
                var timeScore = countDownSystem.CrtRemainSeconds * 10;
                gameModel.Score.Value += timeScore;

                if (gameModel.Score.Value > gameModel.BestScore.Value)
                {
                    Debug.Log($"update best score! {gameModel.Score.Value}");
                    gameModel.BestScore.Value = gameModel.Score.Value;
                }
            });
            
            // 点中enemy加分
            this.RegisterEvent<OnKillEnemyEvent>(e =>
            {
                gameModel.Score.Value += 10;
                Debug.Log($"score add 10, crt score: {gameModel.Score.Value}");
            });

            // 点到其他地方减分
            this.RegisterEvent<OnMissEvent>(e =>
            {
                gameModel.Score.Value -= 5;
                Debug.Log($"score sub 5, crt score: {gameModel.Score.Value}");
            });
        }
    }
}