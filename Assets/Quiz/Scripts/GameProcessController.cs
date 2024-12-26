using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject;

public class GameProcessController : MonoBehaviour, IGameProcess
{
    public Action EndLevels { get; set; }
    
    [SerializeField] private TableView _tableView;
    [SerializeField] private AudioSource _audioSource;

    private LevelsConfigSource _config;
    private IUserInterfaceController _userInterfaceController;

    private readonly List<string> _previousRightAnswer = new List<string>();
    
    private int _currentLevel;
    private string _rightAnswer;

    [Inject]
    private void Constructor(LevelsConfigSource config, IUserInterfaceController userInterfaceController)
    {
        _userInterfaceController = userInterfaceController;
        _config = config;
    }

    public void InitGameProcess()
    {
        _tableView.ClearTable();
    }

    public void StartLevel(int levelIndex, bool isStartGame)
    {
        _currentLevel = levelIndex;

        InitTableView(_config.Levels[_currentLevel]);
        GeneratedRightAnswer();
        
        _userInterfaceController.SetTitle(_rightAnswer, isStartGame);

        if (isStartGame)
        {
            _audioSource.Play();
        }
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
    }

    private void OnSelectedCell(CellView cell)
    {
        if (_rightAnswer == cell.Id)
        {
            cell.CorrectAnswerReaction(EndLevel);
            
        }
        else
        {
            cell.WrongAnswerReaction();
        }
    }

    private void EndLevel()
    {
        _currentLevel++;

        if (_currentLevel >= _config.Levels.Count)
        {
            _previousRightAnswer.Clear();
            
            EndLevels?.Invoke();
            return;
        }
        
        _tableView.ClearTable();
        StartLevel(_currentLevel, false);
    }
}