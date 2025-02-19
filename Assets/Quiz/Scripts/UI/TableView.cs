using System;
using System.Collections.Generic;
using ModestTree;
using Quiz.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = System.Random;

namespace Quiz.UI
{
    public class TableView : MonoBehaviour
    {
        public Action<CellView> SelectedCell { get; set; }
    
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GridLayoutGroup _gridLayout;
    
        private ICellFactory _cellFactory;

        private readonly List<GameObject> _cells = new();
        private readonly List<int> _usedIndexes = new();
    
        private DataConfigSource _dataConfig;

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
            Random randRightAnswerIndex = new();
            int generateIndex = randRightAnswerIndex.Next(_usedIndexes.Count);

            return _dataConfig.Data[_usedIndexes[generateIndex]].Name;
        }
    
        private void CreateCell(DataConfigSource dataConfig, int generateIndex)
        {
            GameObject cell = _cellFactory.CreateCell(this, gameObject.transform, dataConfig.Data[generateIndex], _gridLayout.cellSize);
            _cells.Add(cell);
        }

        public void ClearTable()
        {
            if (_cells.IsEmpty())
            {
                return;
            }
        
            foreach (GameObject cell in _cells)
            {
                _cellFactory.DisposeCell(cell);
            }
        
            _cells.Clear();
        }

        public void OnSelectedCell(CellView cell)
        {
            SelectedCell?.Invoke(cell);
        }
    }
}