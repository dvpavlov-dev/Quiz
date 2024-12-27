using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Quiz.UI
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _titleText;
    
        public void ShowTitleAnimation()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, 1);
        }

        public void SetTitle(string titleText)
        {
            _titleText.text = titleText;
        }
    }
}