using System;
using DG.Tweening;
using UnityEngine;

public class LoadingScreenView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    
    public void ShowLoadingScreen(Action animationEnded)
    {
        _canvasGroup.DOFade(1, 1f).OnComplete(() => animationEnded?.Invoke());
    }

    public void HideLoadingScreen(Action animationEnded)
    {
        if (_canvasGroup.alpha < 1)
        {
            animationEnded?.Invoke();
            return;
        }
        
        _canvasGroup.DOFade(0, 1f).OnComplete(() => animationEnded?.Invoke());
    }
}
