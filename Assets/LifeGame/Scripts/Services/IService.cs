using Cysharp.Threading.Tasks;

namespace LifeGame.Services
{
    public interface IService
    {
        UniTask InitializeAsync();
        UniTask StartAsync();
    }
}