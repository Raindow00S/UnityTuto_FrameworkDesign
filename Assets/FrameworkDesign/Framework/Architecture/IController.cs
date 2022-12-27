namespace FrameworkDesign
{
    // 表现层对象不注册到Architecture，因为对象经常进行创建和销毁
    public interface IController : IBelongToArchitecture, ICanSendCommand, ICanGetModel, ICanGetSystem, ICanRegisterEvent
    {
        // 表现层对象中，访问Architecture中的System或Model就不需要单例形式了
        
    }
}