using System;
using System.Collections.Generic;
using LifeGame.GameData;
using LifeGame.Services.Timer;
using LifeGame.Services.UnityEvent;
using Newtonsoft.Json;
using UnityEngine;

namespace LifeGame.Services.PlayerData
{
    public class DataAccessProvider
    {
        public event Action<int> LivesChanged;
        private readonly Data _data;
        private readonly Config _config;
        private DateTime _pauseTime;
        private ITimeService TimerService => ServiceProvider.TimeService;
        private IUnityEventService UnityEventService => ServiceProvider.UnityEventService;

        public int Lives
        {
            get => _data.Lives;
            set
            {
                _data.Lives = Mathf.Clamp(value, 0, _config.MaxLives);
                LivesChanged?.Invoke(_data.Lives);
            }
        }

        public int Coins
        {
            get => _data.Coins;
            set => _data.Coins = value;
        }

        public List<DateTime> DailyClaimed
        {
            get => _data.DailyClaimed;
        }

        public DateTime QuitTime
        {
            get => _data.QuitTime;
        }

        public TimeService.Timer LivesTimer { get; private set; }

        public DataAccessProvider(Data data, Config config)
        {
            _config = config;
            _data = data;

            InitializeTimer();
            ValidateLives(TimerService.EnterDateTime, QuitTime);
            SubscribeOnEvents();
            InitialCalls();
        }

        private void InitialCalls()
        {
            OnLivesChanged(Lives);
        }

        private void InitializeTimer()
        {
            LivesTimer = TimerService.Create(0);
            LivesTimer.OnOverTime(() => Lives += 1);
        }

        private void SubscribeOnEvents()
        {
            LivesChanged += OnLivesChanged;
            UnityEventService.ApplicationPause += OnApplicationPause;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                _pauseTime = DateTime.Now;
            else
                ValidateLives(DateTime.Now, _pauseTime);
        }

        private void OnLivesChanged(int amount)
        {
            if (amount != _config.MaxLives)
            {
                if (LivesTimer.TimerIsOver)
                    LivesTimer.SetTimeLeft(_config.LifeCooldownSeconds);
            }
            else if (amount >= _config.MaxLives && !LivesTimer.TimerIsOver)
                LivesTimer.ForceStop();
        }

        private void ValidateLives(DateTime enterTime, DateTime quitTime)
        {
            var config = _config;
            var timeDifferenceInSeconds = enterTime.Subtract(quitTime).TotalSeconds;
            int livesToGive = (int)(timeDifferenceInSeconds / config.LifeCooldownSeconds);
            int remainSeconds = (int)(config.LifeCooldownSeconds - (timeDifferenceInSeconds % config.LifeCooldownSeconds));

            Lives += livesToGive;

            if (Lives != config.MaxLives)
                LivesTimer.SetTimeLeft(remainSeconds);
        }
    }
}