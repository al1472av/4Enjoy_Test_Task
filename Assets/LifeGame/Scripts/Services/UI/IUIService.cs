using System.Collections.Generic;
using LifeGame.UI.Popups;
using LifeGame.UI.Windows;

namespace LifeGame.Services.UI
{
    public interface IUIService
    {
        void OpenWindow(WindowBase windowToOpen);
        void OpenWindow<T>() where T : WindowBase;
        void AddWindow(WindowBase window);
        T GetWindow<T>() where T : WindowBase;
        List<T> GetWindows<T>() where T : WindowBase;
        void RemoveWindow(WindowBase window);
        void ShowPopup(PopupBase popup);
        void AddPopup(PopupBase popup);
        void RemovePopup(PopupBase popup);
    }
}