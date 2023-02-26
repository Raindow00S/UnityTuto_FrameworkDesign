using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FrameworkDesign;
using FrameworkDesign.Example;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface IAchievementSystem : ISystem
    {
        
    }

    public class AchievementItem
    {
        public string Name { get; set; }
        public Func<bool> CheckComplete { get; set; }
        public bool Unlocked { get; set; }
    }
    
    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        private List<AchievementItem> mItems = new List<AchievementItem>();
        private bool mMissed = false;

        protected override void OnInit()
        {
            this.RegisterEvent<OnMissEvent>(e =>
            {
                mMissed = true;
            });

            this.RegisterEvent<GameStartEvent>(e =>
            {
                mMissed = false;
            });
            
            mItems.Add(new AchievementItem()
            {
                Name = "百分成就",
                CheckComplete = () => this.GetModel<IGameModel>().Score.Value >= 100
            });
            mItems.Add(new AchievementItem()
            {
                Name = "手残成就",
                CheckComplete = () => this.GetModel<IGameModel>().Score.Value < 0
            });
            mItems.Add(new AchievementItem()
            {
                Name = "零失误成就",
                CheckComplete = () => !mMissed
            });

            this.RegisterEvent<GamePassEvent>(async e =>
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f));

                foreach (var achivementItem in mItems)
                {
                    if (!achivementItem.Unlocked && achivementItem.CheckComplete())
                    {
                        achivementItem.Unlocked = true;
                        Debug.Log($"unlock achievement {achivementItem.Name}");
                    }
                }
            });
        }
    }
}