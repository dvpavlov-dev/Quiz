using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TableView : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GridLayoutGroup _gridLayout;
    
    private ICellFactory _cellFactory;
    
    private readonly List<GameObject> _cells = new List<GameObject>();

    [Inject]
    private void Constructor(ICellFactory cellFactory)
    {
        _cellFactory = cellFactory;
    }
    
    public void CreateTable(int width, int height, DataConfigSource dataConfig)
    {
        _rectTransform.sizeDelta = new Vector2(_gridLayout.cellSize.x * width, _gridLayout.cellSize.y * height);

        for (int i = 0; i < width * height; i++)
        {
            GameObject cell = _cellFactory.CreateCell(gameObject.transform, dataConfig.Data[i].Image);
            _cells.Add(cell);
        }
    }

    public void ClearTable()
    {
        foreach (GameObject cell in _cells)
        {
            Destroy(cell);
        }
        
        _cells.Clear();
    }
}
