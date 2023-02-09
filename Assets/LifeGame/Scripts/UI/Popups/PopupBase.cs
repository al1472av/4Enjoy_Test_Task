using Cysharp.Threading.Tasks;
using LifeGame.Services;
using LifeGame.Services.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace LifeGame.UI.Popups
{
    public class PopupBase : MonoBehaviour, IUIObject
    {
        [SerializeField] protected Button _backgroundAsCloseButton;
        [SerializeField] protected Button _closeButton;
        
        protected IUIFactoryService UIFactoryService => ServiceProvider.UIFactory;
        
        public virtual UniTask Initialize()
        {
            _backgroundAsCloseButton.onClick.AddListener(() => Hide());
            _closeButton.onClick.AddListener(() => Hide());
            return UniTask.CompletedTask;
        }

        public virtual UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public virtual UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        public void HardClose()
        {
            gameObject.SetActive(false);
        }
    }
}