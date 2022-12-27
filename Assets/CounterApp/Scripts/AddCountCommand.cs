using FrameworkDesign;

namespace CounterApp
{
    // 这是个结构体，内存管理效率比class高
    public class AddCountCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<ICounterModel>().Count.Value++;
        }
    }
}