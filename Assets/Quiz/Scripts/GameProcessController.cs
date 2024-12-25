using System;
using UnityEngine;
using Zenject;

public class GameProcessController : MonoBehaviour, IGameProcess
{
    public Action EndLevels { get; set; }
    
    [SerializeField] private TableView _tableView;

    private LevelsConfigSource _config;

    private int _currentLevel = 1;

    [Inject]
    private void Constructor(LevelsConfigSource config)
    {
        _config = config;
    }

    public void StartLevel()
    {
        Level currentLevel = _config.Levels[_currentLevel];
        _tableView.CreateTable(currentLevel.TableWidth, currentLevel.TableHeight, currentLevel.DataConfig);
    }

    private void EndLevel()
    {
        _tableView.ClearTable();
        _currentLevel++;

        if (_config.Levels.Count >= _currentLevel)
        {
            EndLevels?.Invoke();
            return;
        }
        
        StartLevel();
    }
}