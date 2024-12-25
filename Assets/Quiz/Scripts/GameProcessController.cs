using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject;

public class GameProcessController : MonoBehaviour, IGameProcess
{
    public Action EndLevels { get; set; }
    
    [SerializeField] private TableView _tableView;

    private LevelsConfigSource _config;
    private IUserInterfaceController _userInterfaceController;

    private readonly List<string> _previousRightAnswer = new List<string>();
    
    private int _currentLevel = 0;
    private string _rightAnswer;

    [Inject]
    private void Constructor(LevelsConfigSource config, IUserInterfaceController userInterfaceController)
    {
        _userInterfaceController = userInterfaceController;
        _config = config;
    }

    public void StartLevel(int levelIndex)
    {
        _currentLevel = levelIndex;
        Level currentLevel = _config.Levels[_currentLevel];

        InitTableView(currentLevel);
        GeneratedRightAnswer();
    }
    
    private void InitTableView(Level currentLevel)
    {
        _tableView.CreateTable(currentLevel.TableWidth, currentLevel.TableHeight, currentLevel.DataConfig);
        _tableView.SelectedCell = OnSelectedCell;
    }

    private void GeneratedRightAnswer()
    {
        if (_previousRightAnswer.IsEmpty())
        {
            _rightAnswer = _tableView.GenerateRightAnswer();
        }
        else
        {
            while (_previousRightAnswer.Contains(_rightAnswer))
            {
                _rightAnswer = _tableView.GenerateRightAnswer();
            }
        }
        
        _previousRightAnswer.Add(_rightAnswer);
        _userInterfaceController.SetTitle(_rightAnswer);
    }

    private void OnSelectedCell(string id)
    {
        if (_rightAnswer == id)
        {
            EndLevel();
        }
    }

    private void EndLevel()
    {
        _tableView.ClearTable();
        _currentLevel++;

        if (_currentLevel >= _config.Levels.Count)
        {
            _previousRightAnswer.Clear();
            
            EndLevels?.Invoke();
            return;
        }
        
        StartLevel(_currentLevel);
    }
}