using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using LifeGame.UI.Popups;
using LifeGame.UI.Windows;
using UnityEngine;

namespace LifeGame.Services.UI
{
    public class UIService : ServiceBase, IUIService
    {
        private List<WindowBase> _windows;
        private List<PopupBase> _popups;
        private WindowBase _currentWindow;

        public override UniTask InitializeAsync()
        {
            _windows = new List<WindowBase>();
            _popups = new List<PopupBase>();
            return base.InitializeAsync();
        }

        public void OpenWindow(WindowBase windowToOpen)
        {
            OpenWindowInternal(windowToOpen);
        }

        public void OpenWindow<T>() where T : WindowBase
        {
            var windowToOpen = GetWindow<T>();
            OpenWindowInternal(windowToOpen);
        }

        public void AddWindow(WindowBase window)
        {
            if (_windows.Contains(window))
            {
                Debug.LogError("Window is already added");
                return;
            }

            _windows.Add(window);
            window.HardClose();
            window.Initialize();
        }

        public void RemoveWindow(WindowBase window)
        {
            if (_windows.Contains(window))
                _windows.Remove(window);
        }

        public void ShowPopup(PopupBase popup)
        {
            popup.Show();
        }

        public void AddPopup(PopupBase popup)
        {
            _popups.Add(popup);
            popup.Initialize();
        }

        public void RemovePopup(PopupBase popup)
        {
            if (_popups.Contains(popup))
                _popups.Remove(popup);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>If not only one entry of type T, will return the first one</returns>
        public T GetWindow<T>() where T : WindowBase => _windows.OfType<T>().FirstOrDefault();

        public List<T> GetWindows<T>() where T : WindowBase => _windows.OfType<T>().ToList();

        private void OpenWindowInternal(WindowBase windowToOpen)
        {
            if (windowToOpen == _currentWindow)
                return;

            _currentWindow?.Close();
            _currentWindow = windowToOpen;
            _currentWindow.Open();
        }
    }
}