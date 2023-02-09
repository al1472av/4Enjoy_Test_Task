using LifeGame.Services.Timer;

namespace LifeGame.UI.Popups.Lives.Views.Args
{
    public class NoLivesViewArgs : LivesViewArgsBase
    {
        public readonly int LivesAmount;
        public readonly TimeService.Timer Timer;

        public NoLivesViewArgs(int livesAmount, TimeService.Timer timer)
        {
            LivesAmount = livesAmount;
            Timer = timer;
        }
    }
}