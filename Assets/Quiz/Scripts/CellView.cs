using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _targetImage;

    public string Id => _id;
    
    private string _id;
    private TableView _tableView;

    public void Init(TableView tableView, CellData cellData, Vector2 cellSize)
    {
        _tableView = tableView;
        _id = cellData.Name;
        _targetImage.sprite = cellData.Image;

        if (cellData.RotateToVertical)
        {
            _targetImage.rectTransform.rotation = Quaternion.Euler(0,0,-90);
        }

        CalcSizeTargetImage(cellData.Image.rect.width, cellData.Image.rect.height);
        ShowCellAnimation(cellSize);
    }

    public void OnSelectedCell()
    {
        _tableView.OnSelectedCell(this);
    }

    public void CorrectAnswerAnimation(Action animationEnded)
    {
        Vector2 cellSize = _targetImage.rectTransform.sizeDelta;
        _targetImage.rectTransform.sizeDelta = new Vector2(0, 0);
        _targetImage.rectTransform.DOSizeDelta(cellSize, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => animationEnded?.Invoke());
    }

    public void WrongAnswerAnimation()
    {
        _targetImage.rectTransform.DOShakePosition(0.5f, 5);
    }
    
    private void ShowCellAnimation(Vector2 cellSize)
    {
        _rectTransform.DOSizeDelta(new Vector2(0, 0), 0);
        _rectTransform.DOSizeDelta(cellSize, 0.5f).SetEase(Ease.OutBounce);
    }

    private void CalcSizeTargetImage(float width, float height)
    {
        bool isVerticalImage = width < height;
        
        float size = _targetImage.rectTransform.rect.width;
        float calcSizeImage = size * (isVerticalImage ? width / height : height / width);

        _targetImage.rectTransform.SetSizeWithCurrentAnchors(isVerticalImage ? RectTransform.Axis.Horizontal : RectTransform.Axis.Vertical, calcSizeImage);
    }
}
