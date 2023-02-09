using LifeGame.UI.Popups.Lives.Views.Args;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace LifeGame.UI.Popups.Lives.Views
{
    public abstract class LivesPopupViewBase : SerializedMonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _livesAmountText;

        public abstract void SetViewData(LivesViewArgsBase args);

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}