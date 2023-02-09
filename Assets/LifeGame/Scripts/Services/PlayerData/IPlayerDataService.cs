namespace LifeGame.Services.PlayerData
{
    public interface IPlayerDataService : IService
    {
        DataAccessProvider Data { get; }
        void LoadData();
        void SaveData();
    }
}