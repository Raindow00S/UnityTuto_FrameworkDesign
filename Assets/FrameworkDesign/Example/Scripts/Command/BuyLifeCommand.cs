namespace FrameworkDesign.Example
{
    public class BuyLifeCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            // 花费金币买生命
            var gameModel = this.GetModel<IGameModel>();
            gameModel.Gold.Value--;
            gameModel.Life.Value++;
        }
    }
}