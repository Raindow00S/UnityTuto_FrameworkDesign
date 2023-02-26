namespace FrameworkDesign.Example
{
    public class MissCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();
            if (gameModel.Life.Value > 0)
            {
                gameModel.Life.Value--; // 生命值可以抵消点错
            }
            else
            {
                this.SendEvent<OnMissEvent>();
            }
        }
    }
}