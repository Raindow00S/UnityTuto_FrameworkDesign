using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    // 显式实现在接口+静态扩展中的使用
    public class CanDoEverything
    {
        public void DoSomething1()
        {
            Debug.Log("DoSomething1");
        }
        
        public void DoSomething2()
        {
            Debug.Log("DoSomething2");
        }
        
        public void DoSomething3()
        {
            Debug.Log("DoSomething3");
        }
    }

    public interface IHaveEverything
    {
        CanDoEverything CanDoEverything { get; }
    }

    public interface ICanDoSomething1 : IHaveEverything
    {
        
    }

    public static class ICanDoSomething1Exetension
    {
        public static void DoSomething1(this ICanDoSomething1 self)
        {
            self.CanDoEverything.DoSomething1();
        }
    }
    
    public interface ICanDoSomething2 : IHaveEverything
    {
        
    }

    public static class ICanDoSomething2Exetension
    {
        public static void DoSomething2(this ICanDoSomething2 self)
        {
            self.CanDoEverything.DoSomething2();
        }
    }
    
    public interface ICanDoSomething3 : IHaveEverything
    {
        
    }

    public static class ICanDoSomething3Exetension
    {
        public static void DoSomething3(this ICanDoSomething3 self)
        {
            self.CanDoEverything.DoSomething3();
        }
    }

    
    public class InterfaceRuleExample : MonoBehaviour
    {
        public class OnlyCanDo1 : ICanDoSomething1  // 一个接口相当于一个规则
        {
            // 通过显式实现来防止直接通过CanDoEverything做其他事
            CanDoEverything IHaveEverything.CanDoEverything { get; } = new CanDoEverything();
        }

        public class OnlyCanDo23 : ICanDoSomething2, ICanDoSomething3
        {
            CanDoEverything IHaveEverything.CanDoEverything { get; } = new CanDoEverything();
        }

        private void Start()
        {
            var onlyCanDo1 = new OnlyCanDo1();
            // 如果不用显式实现，实际上还是可以通过持有的CanDoEverything去做其他事，这是我们不想要的
            // onlyCanDo1.CanDoEverything.DoSomething2();
            onlyCanDo1.DoSomething1();

            var onlyCanDo23 = new OnlyCanDo23();
            onlyCanDo23.DoSomething2();
            onlyCanDo23.DoSomething3();

        }
    }
}