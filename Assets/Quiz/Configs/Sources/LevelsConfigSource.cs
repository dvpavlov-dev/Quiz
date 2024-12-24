using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/Levels config")]
public class LevelsConfigSource : ScriptableObject
{
    public List<Level> Levels;
}

[Serializable]
public struct Level
{
    public DataConfigSource DataConfig;
    public int TableWidth;
    public int TableHeight;
}