using System;
using Cysharp.Threading.Tasks;
using LifeGame.Services.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LifeGame.Services.SceneLoader
{
    public class SceneLoaderService : ServiceBase, ISceneLoaderService
    {
        private ILoaderService LoadingService => ServiceProvider.Loading;
        
        public async UniTask Load(string sceneName, Action onLoaded = null)
        {
            var sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName);
            await LoadingService.Load(new SceneLoadingOperation(sceneLoadingOperation, "Loading level"));
            onLoaded?.Invoke();
        }

        private class SceneLoadingOperation : ILoadingOperation
        {
            public string Description { get; }
            
            private readonly AsyncOperation _sceneLoadingOperation;

            public SceneLoadingOperation(AsyncOperation sceneLoadingOperation, string description)
            {
                _sceneLoadingOperation = sceneLoadingOperation;
                Description = description;
            }
            
            public async UniTask Load(Action<float> onProgress)
            {
                if (_sceneLoadingOperation == null)
                    throw new NullReferenceException("Scene Loading Operation is null");

                while (!_sceneLoadingOperation.isDone)
                {
                    onProgress?.Invoke(_sceneLoadingOperation.progress);
                    await UniTask.Delay(1);
                }

            }
        }

    }
}