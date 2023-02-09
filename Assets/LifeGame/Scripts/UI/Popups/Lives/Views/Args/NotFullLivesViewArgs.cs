using LifeGame.Services.Timer;

namespace LifeGame.UI.Popups.Lives.Views.Args
{
    public class NotFullLivesViewArgs : LivesViewArgsBase
    {
        public readonly int LivesAmount;
        public readonly TimeService.Timer Timer;

        public NotFullLivesViewArgs(int livesAmount, TimeService.Timer timer)
        {
            LivesAmount = livesAmount;
            Timer = timer;
        }
    }
}