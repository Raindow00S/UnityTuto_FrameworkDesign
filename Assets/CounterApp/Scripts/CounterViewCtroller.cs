using FrameworkDesign;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterViewCtroller : MonoBehaviour, IController
    {
        private ICounterModel mCounterModel;
        
        private void Start()
        {
            // 会造成循环调用堆栈溢出
            // mCounterModel = CounterApp.Get<ICounterModel>();
            mCounterModel = this.GetModel<ICounterModel>();
            
            mCounterModel.Count.OnValueChanged += OnCountChanged;

            transform.Find("BtnAdd").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<AddCountCommand>();
                // new AddCountCommand().Execute();
            });
            
            transform.Find("BtnSub").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<SubCountCommand>();
                // new SubCountCommand().Execute();
            });
            
            OnCountChanged(mCounterModel.Count.Value);
        }

        private void OnCountChanged(int newCount)
        {
            transform.Find("TextCount").GetComponent<TextMeshProUGUI>().text = newCount.ToString();
        }

        private void OnDestroy()
        {
            mCounterModel.Count.OnValueChanged -= OnCountChanged;
            mCounterModel = null;
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            // 初始化Architecture需要可以获取CounterApp对象，所以CounterApp需要是一个类似单例类的形式
            return CounterApp.Interface;
        }
    }

    public interface ICounterModel : IModel
    {
        BindableProperty<int> Count { get; }
    }

    public class CounterModel : AbstractModel, ICounterModel
    {
        public BindableProperty<int> Count { get; } = new BindableProperty<int>() {Value = 0};

        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();
            
            Count.Value = storage.LoadInt("COUNTER_COUNT", 0);
            Count.OnValueChanged += count =>
            {
                storage.SaveInt("COUNTER_COUNT", count);
            };
        }

        // public CounterModel()
        // {
        //     // 循环访问
        //     // var storage = CounterApp.Get<IStorage>();
        //     var storage = Architecture.GetUtility<IStorage>();
        //     
        //     Count.Value = storage.LoadInt("COUNTER_COUNT", 0);
        //     Count.OnValueChanged += count =>
        //     {
        //         storage.SaveInt("COUNTER_COUNT", count);
        //     };
        // }
        
    }
}