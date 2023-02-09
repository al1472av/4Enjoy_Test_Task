using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace LifeGame.Services.Loading
{
    public interface ILoaderService : IService
    {
        UniTask Load(Queue<ILoadingOperation> loadingOperations);
        UniTask Load(ILoadingOperation loadingOperation);
    }
}