using System;
using LifeGame.Services.Timer;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LifeGame.UI.Windows.MainMenu
{
    public class LivesBarView : SerializedMonoBehaviour
    {
        //Need to be made as Translate ID for localization, not just text
        private const string FULL_TEXT = "FULL";
        [OdinSerialize] public Button Button { get; private set; }
        [SerializeField] private TextMeshProUGUI _livesAmount;
        [SerializeField] private TextMeshProUGUI _remainTime;

        public void Initialize(TimeService.Timer timer)
        {
            timer.OnUpdate(OnTimeUpdate);
            timer.OnOverTime(OnTimerOver);
        }

        public void SetViewData(int livesAmount)
        {
            _livesAmount.text = livesAmount.ToString();
            _remainTime.text = FULL_TEXT;
        }

        private void OnTimeUpdate(float value)
        {
            var span = TimeSpan.FromSeconds(value);
            _remainTime.text = $"{span.Minutes:00}:{span.Seconds:00}";
        }

        private void OnTimerOver()
        {
            _remainTime.text = FULL_TEXT;
        }
    }
}