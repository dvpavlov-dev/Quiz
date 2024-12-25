using UnityEngine;

public class CellFactory : MonoBehaviour, ICellFactory
{
    [SerializeField] private GameObject _cellPrefab;
    
    public GameObject CreateCell(TableView tableView, Transform parent, CellData cellData)
    {
        GameObject cell = Instantiate(_cellPrefab, parent);
        cell.GetComponent<CellView>().Init(tableView, cellData);
        
        return cell;
    }
}

public interface ICellFactory
{
    public GameObject CreateCell(TableView tableView, Transform parent, CellData cellData);
}