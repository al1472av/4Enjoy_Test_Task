using System;

namespace LifeGame.Services.Timer
{
    public interface ITimeService : IService
    {
        DateTime EnterDateTime { get; }
        TimeService.Timer Create(float timeLeft);
    }
}