using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = System.Random;

public class TableView : MonoBehaviour
{
    public Action<string> SelectedCell { get; set; }
    
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GridLayoutGroup _gridLayout;
    
    private ICellFactory _cellFactory;
    
    private readonly List<GameObject> _cells = new List<GameObject>();
    private DataConfigSource _dataConfig;
    private List<int> _usedIndexes = new List<int>();

    [Inject]
    private void Constructor(ICellFactory cellFactory)
    {
        _cellFactory = cellFactory;
    }
    
    public void CreateTable(int width, int height, DataConfigSource dataConfig)
    {
        _dataConfig = dataConfig;
        _rectTransform.sizeDelta = new Vector2(_gridLayout.cellSize.x * width, _gridLayout.cellSize.y * height);
        
        _usedIndexes.Clear();
        Random randIndex = new Random();
        
        for (int i = 0; i < width * height;)
        {
            int generateIndex = randIndex.Next(dataConfig.Data.Count);

            if (!_usedIndexes.Contains(generateIndex))
            {
                _usedIndexes.Add(generateIndex);
                CreateCell(dataConfig, generateIndex);
                i++;
            }
        }
    }

    public string GenerateRightAnswer()
    {
        Random randRightAnswerIndex = new Random();
        int generateIndex = randRightAnswerIndex.Next(_usedIndexes.Count);

        return _dataConfig.Data[_usedIndexes[generateIndex]].Name;
    }
    
    private void CreateCell(DataConfigSource dataConfig, int generateIndex)
    {
        GameObject cell = _cellFactory.CreateCell(this, gameObject.transform, dataConfig.Data[generateIndex]);
        _cells.Add(cell);
    }

    public void ClearTable()
    {
        foreach (GameObject cell in _cells)
        {
            Destroy(cell);
        }
        
        _cells.Clear();
    }

    public void OnSelectedCell(CellView cell)
    {
        SelectedCell?.Invoke(cell.Id);
    }
}
