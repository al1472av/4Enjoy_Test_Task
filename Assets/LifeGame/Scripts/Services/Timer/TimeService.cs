using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LifeGame.Services.Timer
{
    public class TimeService : ServiceBase, ITimeService
    {
        public DateTime EnterDateTime { get; private set; }
        private List<Timer> _timers;

        public override UniTask InitializeAsync()
        {
            EnterDateTime = DateTime.Now;
            _timers = new List<Timer>();
            return base.InitializeAsync();
        }

        public Timer Create(float timeLeft)
        {
            var timer = new Timer();
            _timers.Add(timer);
            timer.SetTimeLeft(timeLeft);
            return timer;
        }
        
        private void Update()
        {
            foreach (var timer in _timers)
            {
                if (timer.TimeLeft >= 0)
                    timer.SubtractDelta(Time.deltaTime);
            }

        }

        public class Timer
        {
            public float TimeLeft { get; private set; }
            private event Action<float> Update;
            private event Action OverTime;
            public bool TimerIsOver => TimeLeft <= 0;

            public void SetTimeLeft(float timeLeft)
            {
                TimeLeft = timeLeft;
            }

            public void SubtractDelta(float deltaTime)
            {
                TimeLeft -= deltaTime;
                Update?.Invoke(TimeLeft);

                if (TimeLeft <= 0)
                {
                    OverTime?.Invoke();
                }
            }

            public Timer OnUpdate(Action<float> callback)
            {
                Update += callback;
                return this;
            }

            public Timer OnOverTime(Action callback)
            {
                OverTime += callback;
                return this;
            }

            public void ForceStop()
            {
                TimeLeft = -1;
            }
        }
    }
}