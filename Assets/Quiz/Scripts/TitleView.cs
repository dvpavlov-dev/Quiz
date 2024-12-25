using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _titleText;
    
    public void ShowTitleAnimation()
    {
        _background.DOFade(0, 0);
        _titleText.DOFade(0, 0);
        
        _background.DOFade(0.15f, 1f);
        _titleText.DOFade(1, 1f);
    }

    public void SetTitle(string titleText)
    {
        _titleText.text = titleText;
    }
}
