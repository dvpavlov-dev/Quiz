using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private Image _targetImage;

    public void Init(Sprite targetImage)
    {
        _targetImage.sprite = targetImage;
    }
}
