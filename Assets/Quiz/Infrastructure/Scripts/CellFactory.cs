using UnityEngine;

public class CellFactory : MonoBehaviour, ICellFactory
{
    public GameObject CreateCell()
    {
        return null;
    }
}

public interface ICellFactory
{
    public GameObject CreateCell();
}