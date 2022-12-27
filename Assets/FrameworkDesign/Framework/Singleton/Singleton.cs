using System;
using System.Reflection;

namespace FrameworkDesign
{
    public class Singleton<T> where T : Singleton<T>
    {
        private static T mInstance;

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    var type = typeof(T);
                    // 找到T类的所有非公共的构造函数
                    var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    // 找到参数数量为0的函数
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    if (ctor == null)
                    {
                        throw new Exception("Non Public Consturctor Not Found in " + type.Name);
                    }

                    mInstance = ctor.Invoke(null) as T;
                }

                return mInstance;
            }
        }
    }
}