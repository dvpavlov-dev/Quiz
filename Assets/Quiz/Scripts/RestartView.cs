using System;
using DG.Tweening;
using UnityEngine;

public class RestartView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup; 

    public void ShowRestartView()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 0.5f);
    }

    public void HideRestartView(Action endAnimation)
    {
        _canvasGroup.DOFade(0, 0.5f).OnComplete(() => endAnimation?.Invoke());
    }
}