using FrameworkDesign.Example;

namespace FrameworkDesign
{
    public interface IModel : IBelongToArchitecture, ICanSetArchitecture, ICanGetUtility, ICanSendEvent
    {
        void Init();
    }
    
    public abstract class AbstractModel : IModel
    {
        private IArchitecture mArchitecture;

        void IModel.Init()
        {
            OnInit();
        }

        // model层不应该可以发送命令，所以不应该可以访问到mArchitecture
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        protected abstract void OnInit();
    }
}