using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenView : MonoBehaviour
{
    [SerializeField] private Image _loadingScreenImage;

    private bool _isLoadingScreenShowing;
    
    public void ShowLoadingScreen(Action animationEnded)
    {
        _isLoadingScreenShowing = true;
        _loadingScreenImage.DOFade(1, 1f).OnComplete(() => animationEnded?.Invoke());
    }

    public void HideLoadingScreen(Action animationEnded)
    {
        if (!_isLoadingScreenShowing)
        {
            animationEnded?.Invoke();
            return;
        }
        
        _loadingScreenImage.DOFade(0, 1f).OnComplete(() => animationEnded?.Invoke());
    }
}
