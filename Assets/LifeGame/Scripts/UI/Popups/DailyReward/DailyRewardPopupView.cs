using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LifeGame.UI.Popups.DailyReward
{
    public class DailyRewardPopupView : SerializedMonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI _coinsAmountText;
        [OdinSerialize] public Button Button { get; private set; }

        public void SetViewData(int coinsAmount)
        {
            _coinsAmountText.text = coinsAmount.ToString();
        }
    }
}