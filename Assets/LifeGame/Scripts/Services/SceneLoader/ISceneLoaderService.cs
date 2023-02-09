using System;
using Cysharp.Threading.Tasks;

namespace LifeGame.Services.SceneLoader
{
    public interface ISceneLoaderService : IService
    {
        UniTask Load(string sceneName, Action onLoaded = null);
    }
}