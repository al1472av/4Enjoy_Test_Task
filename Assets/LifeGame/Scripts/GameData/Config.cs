using System;
using System.Collections.Generic;
using System.Linq;
using LifeGame.Services.Timer;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace LifeGame.GameData
{
    [CreateAssetMenu(menuName = "Life Game/Config")]
    public class Config : SerializedScriptableObject
    {
        [SerializeField] private float[] _initialDailyRewardValue;
        [SerializeField] private float _maxDailyReward;
        [OdinSerialize, PropertyRange(2, 20)] public int MaxLives { get; private set; }
        [OdinSerialize] public int MinLives { get; private set; }
        [OdinSerialize] public float LifeCooldownSeconds { get; private set; }


        //Incorrect algorithm - goes to infinity, so i decided to clamp it with _maxDailyReward value
        public int GetDailyRewardByDate(DateTime dateTime)
        {
            List<float> temp = new List<float>(_initialDailyRewardValue);

            int dayInSeason = Seasons.DayInSeason(dateTime);

            if (dayInSeason > _initialDailyRewardValue.Length)
            {
                for (int i = _initialDailyRewardValue.Length; i < dayInSeason; i++)
                {
                    //Remove Mathf.Clamp if you want to test it without clamping
                    //as it was mentioned in task (60% of yesterdays and 100% of the day before)
                    float value = Mathf.Clamp(temp[i - 1] * 0.6f + temp[i - 2], 0, _maxDailyReward);
                    temp.Add(value);
                }
            }

            return (int)temp[dayInSeason - 1];
        }

        public int GetDailyRewardForToday()
        {
            return GetDailyRewardByDate(DateTime.Now);
        }
    }
}