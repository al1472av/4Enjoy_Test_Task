using LifeGame.UI.Popups.Lives.Views.Args;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LifeGame.UI.Popups.Lives.Views
{
    public class FullLivesPopupView : LivesPopupViewBase
    {
        [OdinSerialize] public Button UseLifeButton { get; private set; }

        public override void SetViewData(LivesViewArgsBase args)
        {
            var currentArgs = args as FullLivesViewArgs;
            _livesAmountText.text = currentArgs.LivesAmount.ToString();
        }
    }
}