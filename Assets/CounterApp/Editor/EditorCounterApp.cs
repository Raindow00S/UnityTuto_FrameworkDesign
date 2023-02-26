using FrameworkDesign;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace CounterApp.Editor
{
    /// <summary>
    /// eidtor窗口版本的CounterApp
    /// </summary>
    public class EditorCounterApp : EditorWindow, IController
    {
        [MenuItem(("EditorCounterApp/Open"))]
        static void Open()
        {
            // 不起作用，因为CounterModel已经借助CounterApp里的PlayerPrefsStorage初始化完了
            // CounterApp.Register<IStorage>(new EditorPrefsStorage());

            CounterApp.OnRegisterPatch += app =>
            {
                app.RegisterUtility<IStorage>(new EditorPrefsStorage());
            };
            
            
            var window = GetWindow<EditorCounterApp>();
            window.position = new Rect(100, 100, 400, 600);
            window.titleContent = new GUIContent(nameof(EditorCounterApp));
            window.Show();
        }
        
        private void OnGUI()
        {
            if (GUILayout.Button("+"))
            {
                this.SendCommand<AddCountCommand>();
                // new AddCountCommand().Execute();
            }

            GUILayout.Label(CounterApp.Interface.GetModel<ICounterModel>().Count.Value.ToString());
            
            if (GUILayout.Button("-"))
            {
                this.SendCommand<SubCountCommand>();
                // new SubCountCommand().Execute();
            }
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}