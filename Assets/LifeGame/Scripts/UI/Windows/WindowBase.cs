using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LifeGame.Services;
using LifeGame.Services.Addressable;
using LifeGame.Services.Factory;
using Sirenix.Serialization;
using UnityEngine;

namespace LifeGame.UI.Windows
{
    public class WindowBase : MonoBehaviour, IUIObject
    {
        protected IUIFactoryService UIFactoryService => ServiceProvider.UIFactory;
        protected IAddressablesService AddressablesService => ServiceProvider.Addressables;

        public virtual UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public void HardClose()
        {
            gameObject.SetActive(false);
        }
    }
}
