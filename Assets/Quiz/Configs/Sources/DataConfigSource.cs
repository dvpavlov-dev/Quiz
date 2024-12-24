using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataConfig", menuName = "Configs/Data config")]
public class DataConfigSource : ScriptableObject
{
    public List<CellData> Data;
}

[Serializable]
public struct CellData
{
    public string Name;
    public Sprite Image;
}