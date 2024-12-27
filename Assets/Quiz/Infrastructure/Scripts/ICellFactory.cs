using Quiz.UI;
using UnityEngine;

namespace Quiz.Infrastructure
{
    public interface ICellFactory
    {
        public GameObject CreateCell(TableView tableView, Transform parent, CellData cellData, Vector2 cellSize);

        public void DisposeCell(GameObject cell);
    }
}
