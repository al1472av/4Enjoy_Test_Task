using System;

namespace LifeGame.Services.UnityEvent
{
    public class UnityEventService : ServiceBase, IUnityEventService
    {
        public event Action Updating;
        public event Action FixedUpdating;
        public event Action<bool> ApplicationFocus;
        public event Action<bool> ApplicationPause;
        public event Action ApplicationQuit;

        private void Update()
        {
            Updating?.Invoke();
        }

        private void FixedUpdate()
        {
            FixedUpdating?.Invoke();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            ApplicationFocus?.Invoke(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            ApplicationPause?.Invoke(pauseStatus);
        }

        private void OnApplicationQuit()
        {
            ApplicationQuit?.Invoke();
        }
    }
}