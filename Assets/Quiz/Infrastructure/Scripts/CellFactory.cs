using UnityEngine;

public class CellFactory : MonoBehaviour, ICellFactory
{
    [SerializeField] private GameObject _cellPrefab;
    
    public GameObject CreateCell(Transform parent, Sprite targetImage)
    {
        GameObject cell = Instantiate(_cellPrefab, parent);
        cell.GetComponent<CellView>().Init(targetImage);
        
        return cell;
    }
}

public interface ICellFactory
{
    public GameObject CreateCell(Transform parent, Sprite targetImage);
}