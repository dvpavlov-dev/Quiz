using System.Collections.Generic;
using Quiz.UI;
using UnityEngine;

namespace Quiz.Infrastructure
{
    public class CellFactory : MonoBehaviour, ICellFactory
    {
        [SerializeField] private GameObject _cellPrefab;

        private readonly Queue<GameObject> _objectsPool = new();

        private void Start()
        {
            CreateCells();
        }

        public GameObject CreateCell(TableView tableView, Transform parent, CellData cellData, Vector2 cellSize)
        {
            if (_objectsPool.Count == 0)
            {
                CreateCell();
            }

            return GetCell(tableView, parent, cellData, cellSize);
        }

        public void DisposeCell(GameObject cell)
        {
            cell.SetActive(false);
            _objectsPool.Enqueue(cell);
        }

        private GameObject GetCell(TableView tableView, Transform parent, CellData cellData, Vector2 cellSize)
        {
            GameObject cell = _objectsPool.Dequeue();

            cell.SetActive(true);
            cell.transform.SetParent(parent);
            cell.transform.localScale = Vector3.one;
            cell.GetComponent<CellView>().Init(tableView, cellData, cellSize);

            return cell;
        }

        private void CreateCells()
        {
            for (int i = 0; i < 9; i++)
            {
                CreateCell();
            }
        }

        private void CreateCell()
        {
            GameObject cell = Instantiate(_cellPrefab);
            cell.SetActive(false);

            _objectsPool.Enqueue(cell);
        }
    }

}