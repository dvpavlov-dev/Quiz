using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/Levels config")]
public class LevelsConfigSource : ScriptableObject
{
    [SerializeField] private List<Level> _levels;

    public List<Level> Levels => _levels;
}

[Serializable]
public struct Level
{
    public DataConfigSource DataConfig;
    public int TableWidth;
    public int TableHeight;
}