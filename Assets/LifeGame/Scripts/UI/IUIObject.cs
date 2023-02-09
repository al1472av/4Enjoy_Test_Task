using Cysharp.Threading.Tasks;

namespace LifeGame.UI
{
    public interface IUIObject
    {
        UniTask Initialize();

        void HardClose();
    }
}