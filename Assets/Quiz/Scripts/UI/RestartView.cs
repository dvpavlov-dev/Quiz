using System;
using DG.Tweening;
using UnityEngine;

namespace Quiz.UI
{
    public class RestartView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private AudioSource _audioSource;

        public void ShowRestartView()
        {
            _audioSource.Play();
        
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 0.5f);
        }

        public void HideRestartView(Action endAnimation)
        {
            _canvasGroup.DOFade(0, 0.5f).OnComplete(() => endAnimation?.Invoke());
        }
    }
}