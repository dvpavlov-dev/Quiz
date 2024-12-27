using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz.UI
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _targetImage;
    
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _correctAnswerSound;
        [SerializeField] private AudioClip _wrongAnswerSound;
        
        public string Id => _id;
    
        private string _id;
        [CanBeNull] private TableView _tableView;

        public void Init(TableView tableView, CellData cellData, Vector2 cellSize)
        {
            _tableView = tableView;
            _id = cellData.Name;
            _targetImage.sprite = cellData.Image;

            _targetImage.rectTransform.rotation = Quaternion.Euler(0, 0, cellData.RotateToVertical ? -90 : 0);

            CalcSizeTargetImage(cellSize.x, cellData.Image.rect.width, cellData.Image.rect.height);
            ShowCellAnimation(cellSize);
        }

        public void OnSelectedCell()
        {
            if(_tableView != null)
            {
                _tableView.OnSelectedCell(this);
            }
        }

        public void CorrectAnswerReaction(Action animationEnded)
        {
            _audioSource.clip = _correctAnswerSound;
            _audioSource.Play();
        
            Vector2 cellSize = _targetImage.rectTransform.sizeDelta;
            _targetImage.rectTransform.sizeDelta = new Vector2(0, 0);
            _targetImage.rectTransform.DOSizeDelta(cellSize, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => animationEnded?.Invoke());
        }

        public void WrongAnswerReaction()
        {
            _audioSource.clip = _wrongAnswerSound;
            _audioSource.Play();
        
            _targetImage.rectTransform.DOShakePosition(0.5f, 5);
        }
    
        private void ShowCellAnimation(Vector2 cellSize)
        {
            _rectTransform.DOSizeDelta(new Vector2(0, 0), 0);
            _rectTransform.DOSizeDelta(cellSize, 0.5f).SetEase(Ease.OutBounce);
        }

        private void CalcSizeTargetImage(float cellSize, float width, float height)
        {
            float padding = cellSize * 0.2f;
            bool isVerticalImage = width < height;
        
            float size = cellSize - padding * 2;
            float calcSizeImage = (size - size * (isVerticalImage ? width / height : height / width)) / 2 + padding;

            _targetImage.rectTransform.offsetMin = 
                isVerticalImage ? new Vector2(calcSizeImage, padding) : new Vector2(padding, calcSizeImage);
        
            _targetImage.rectTransform.offsetMax = 
                isVerticalImage ? new Vector2(-calcSizeImage, -padding) : new Vector2(-padding, -calcSizeImage);
        
        }
    }
}