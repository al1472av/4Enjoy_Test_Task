using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LifeGame.Services.Loading
{
    public class LoadingPanel : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private float _barSpeed;

        private float _targetProgress;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Hide();
        }

        public async UniTask Load(Queue<ILoadingOperation> loadingOperations)
        {
            Show();
            StartCoroutine(UpdateSlider());

            foreach (var operation in loadingOperations)
            {
                ResetSlider();
                _description.text = operation.Description;

                await operation.Load(OnProgress);
                await UniTask.WaitUntil(() => _progressBar.value >= _targetProgress);
            }

            Hide();
        }

        private void Show()
        {
            _canvas.enabled = true;
        }

        private void Hide()
        {
            _canvas.enabled = false;
        }

        private IEnumerator UpdateSlider()
        {
            while (_canvas.enabled)
            {
                if (_progressBar.value < _targetProgress)
                {
                    _progressBar.value += Time.deltaTime * _barSpeed;
                    _progressText.text = $"{Mathf.Round(_progressBar.value * 100)}%";
                }

                yield return null;
            }
        }

        private void ResetSlider()
        {
            _progressBar.value = 0;
            _targetProgress = 0;
        }

        private void OnProgress(float progress)
        {
            _targetProgress = progress;
        }
    }
}