using System;

namespace FrameworkDesign
{
    // T需要可比较
    public class BindableProperty<T> where T : IEquatable<T>
    {
        private T mValue = default(T);

        public T Value
        {
            get
            {
                return mValue;
            }
            set
            {
                // 值有变化时，调用委托
                if (!value.Equals(mValue))
                {
                    mValue = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }

        public Action<T> OnValueChanged;
    }
}