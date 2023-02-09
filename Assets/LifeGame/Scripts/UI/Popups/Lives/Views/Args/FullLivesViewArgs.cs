namespace LifeGame.UI.Popups.Lives.Views.Args
{
    public class FullLivesViewArgs : LivesViewArgsBase
    {
        public readonly int LivesAmount;

        public FullLivesViewArgs(int livesAmount)
        {
            LivesAmount = livesAmount;
        }
    }
}