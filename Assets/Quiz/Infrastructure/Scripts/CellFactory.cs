using UnityEngine;

public class CellFactory : MonoBehaviour, ICellFactory
{
    [SerializeField] private GameObject _cellPrefab;
    
    public GameObject CreateCell(TableView tableView, Transform parent, CellData cellData, Vector2 cellSize)
    {
        GameObject cell = Instantiate(_cellPrefab, parent);
        cell.GetComponent<CellView>().Init(tableView, cellData, cellSize);
        
        return cell;
    }
}

public interface ICellFactory
{
    public GameObject CreateCell(TableView tableView, Transform parent, CellData cellData, Vector2 cellSize);
}