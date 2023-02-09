using System;
using LifeGame.UI.Popups.Lives.Views.Args;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LifeGame.UI.Popups.Lives.Views
{
    public class NoLivesPopupView : LivesPopupViewBase
    {
        [SerializeField] protected TextMeshProUGUI _remainTime;
        [OdinSerialize] public Button RefillLifeButton { get; private set; }

        public override void SetViewData(LivesViewArgsBase args)
        {
            var currentArgs = args as NoLivesViewArgs;
            _livesAmountText.text = currentArgs.LivesAmount.ToString();
            currentArgs.Timer.OnUpdate(OnTimeUpdate);
        }

        private void OnTimeUpdate(float value)
        {
            var span = TimeSpan.FromSeconds(value);
            _remainTime.text = $"{span.Minutes:00}:{span.Seconds:00}";
        }
    }
}