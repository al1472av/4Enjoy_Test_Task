using System;
using Cysharp.Threading.Tasks;

namespace LifeGame.Services.Loading
{
    public interface ILoadingOperation
    {
        string Description { get; }
        public UniTask Load(Action<float> onProgress);
    }
}