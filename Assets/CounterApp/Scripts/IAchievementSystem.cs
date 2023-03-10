using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FrameworkDesign;
using FrameworkDesign.Example;
using UnityEngine;

namespace CounterApp
{
    public interface IAchievementSystem : ISystem
    {
        
    }

    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        protected override void OnInit()
        {
            var counterModel = this.GetModel<ICounterModel>();
            var previousCount = counterModel.Count.Value;
            counterModel.Count.RegisterOnValueChanged(newCount =>
            {
                if (previousCount < 10 && newCount >= 10)
                {
                    Debug.Log("成就：点上10！");
                }
                else if (previousCount < 20 && newCount >= 20)
                {
                    Debug.Log("成就：点上20！");
                }
                previousCount = newCount;
            });
        }
    }
}