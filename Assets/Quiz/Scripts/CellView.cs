using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _targetImage;

    public string Id => _id;
    
    private string _id;
    private TableView _tableView;

    public void Init(TableView tableView, CellData cellData)
    {
        _tableView = tableView;
        _id = cellData.Name;
        _targetImage.sprite = cellData.Image;

        if (cellData.RotateToVertical)
        {
            _targetImage.rectTransform.rotation = Quaternion.Euler(0,0,-90);
        }
        
        _targetImage.rectTransform.sizeDelta = CalcSizeTargetImage(cellData.Image.rect.width, cellData.Image.rect.height);
    }
    
    public void OnSelectedCell()
    {
        _tableView.OnSelectedCell(this);
    }

    private Vector2 CalcSizeTargetImage(float width, float height)
    {
        bool isVerticalImage = width < height;
        
        float size = _rectTransform.rect.width - 20 - 20;
        float calcSizeImage = size * (isVerticalImage ? width / height : height / width);
        return isVerticalImage ? new Vector2(calcSizeImage, size) : new Vector2(size, calcSizeImage);
    }
}
