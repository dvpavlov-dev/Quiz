using System;
using DG.Tweening;
using UnityEngine;

public class LoadingScreenView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _spinnerTransform;

    private Tween _rotateTween;
    
    public void ShowLoadingScreen(Action animationEnded)
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 1f).OnComplete(() => animationEnded?.Invoke());
        
        _rotateTween ??= _spinnerTransform
            .DORotate(new Vector3(360, 0, 0), 1f, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.Linear).SetLoops(-1);

        _rotateTween.Play();
    }

    public void HideLoadingScreen(Action animationEnded)
    {
        _rotateTween.Pause();

        _canvasGroup.DOFade(0, 1f).OnComplete(() => animationEnded?.Invoke());
    }
}
