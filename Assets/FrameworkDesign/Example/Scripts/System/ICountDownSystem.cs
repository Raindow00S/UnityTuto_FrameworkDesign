using System;

namespace FrameworkDesign.Example
{
    public interface ICountDownSystem : ISystem
    {
        int CrtRemainSeconds { get; }
        
        void Update();
    }

    public class CountDownSystem : AbstractSystem, ICountDownSystem
    {
        protected override void OnInit()
        {
            this.RegisterEvent<GameStartEvent>(e =>
            {
                mStarted = true;
                mGameStartTime = DateTime.Now;
            });
        }

        private int totalSecounds = 10;
        public int CrtRemainSeconds => totalSecounds - (int) (DateTime.Now - mGameStartTime).TotalSeconds;
        private DateTime mGameStartTime { get; set; }
        private bool mStarted = false;
        
        
        public void Update()
        {
            if (mStarted)
            {
                if (DateTime.Now - mGameStartTime > TimeSpan.FromSeconds(totalSecounds))
                {
                    this.SendEvent<OnCountDownEndEvent>();
                    mStarted = false;
                }
            }
        }
    }
}