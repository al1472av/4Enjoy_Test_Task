using System;

namespace LifeGame.Services.UnityEvent
{
    public interface IUnityEventService : IService
    {
        event Action Updating;
        event Action FixedUpdating;
        event Action<bool> ApplicationFocus;
        event Action<bool> ApplicationPause;
        event Action ApplicationQuit;
    }
}