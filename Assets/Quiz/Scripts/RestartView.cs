using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RestartView : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Button _restartButton;

    public void ShowRestartView()
    {
        _restartButton.gameObject.SetActive(true);
        
        float alphaBackground = _background.color.a;

        _background.DOFade(0, 0);
        _background.DOFade(0.35f, 1f);
    }

    public void HideRestartView(Action endAnimation)
    {
        _background.DOFade(0, 1).OnComplete(() => endAnimation?.Invoke());
    }
}