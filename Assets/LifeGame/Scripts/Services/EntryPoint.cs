using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LifeGame.Services.StateMachine.States;
using UnityEngine;

namespace LifeGame.Services
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ServiceBase[] _services;

        private void Awake()
        {
            if (ValidateEntryPoint())
                Initialize().Forget();
        }

        private bool ValidateEntryPoint()
        {
            int numMusicPlayers = FindObjectsOfType<EntryPoint>().Length;

            if (numMusicPlayers != 1)
            {
                Destroy(gameObject);
                return false;
            }

            return true;
        }

        private async UniTask Initialize()
        {
            DontDestroyOnLoad(this);
           
            SetApplicationSettings();
            
            await AddAndRunServices();
            
            RunGame();
        }

        private static void RunGame()
        {
            ServiceProvider.StateMachine.Enter<AppStartupState>();
        }

        private async UniTask AddAndRunServices()
        {
            ServiceLocator.Clear();

            foreach (var service in _services)
            {
                await service.InitializeAsync();
            }

            foreach (var service in _services)
            {
                ServiceLocator.AddService(service);
                await service.StartAsync();
            }
        }

        private static void SetApplicationSettings()
        {
            Application.targetFrameRate = 120;
        }
    }
}