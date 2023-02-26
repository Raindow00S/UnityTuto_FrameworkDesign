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

            // 游戏结束时计算最高分
            this.RegisterEvent<GamePassEvent>(e =>
            {
                // 先用随机数代替具体分数
                gameModel.Score.Value = Random.Range(0, 50);
                Debug.Log($"score: {gameModel.Score.Value} best score: {gameModel.BestScore.Value}");
                if (gameModel.Score.Value > gameModel.BestScore.Value)
                {
                    Debug.Log("update best score");
                    gameModel.BestScore.Value = gameModel.Score.Value;
                }
            });
        }
    }
}