using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataConfig", menuName = "Configs/Data config")]
public class DataConfigSource : ScriptableObject
{
    [SerializeField] private List<CellData> _data;

    public List<CellData> Data => _data;
}

[Serializable]
public struct CellData
{
    public string Name;
    public Sprite Image;
    public bool RotateToVertical;
}