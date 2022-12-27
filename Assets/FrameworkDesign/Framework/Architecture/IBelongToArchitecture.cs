namespace FrameworkDesign
{
    /// <summary>
    /// 防止architecture循环调用
    /// </summary>
    public interface IBelongToArchitecture
    {
        IArchitecture GetArchitecture();
    }
}