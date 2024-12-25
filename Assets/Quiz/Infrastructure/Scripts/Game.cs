using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Quiz.Infrastructure
{
    public class Game : MonoBehaviour
    {
        private IGameProcess _gameProcess;
        private IUserInterfaceController _userInterfaceController;

        [Inject]
        private void Constructor(IGameProcess gameProcess, IUserInterfaceController userInterfaceController)
        {
            _userInterfaceController = userInterfaceController;
            _gameProcess = gameProcess;

        }
        
        private void Start()
        {
            GameStateMachine gameStateMachine = new GameStateMachine(_gameProcess, _userInterfaceController);
            gameStateMachine.Enter<StartGame>();
        }
    }

    public class GameStateMachine
    {
        private IState _currentState;

        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine(IGameProcess gameProcess, IUserInterfaceController userInterfaceController)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(StartGame)] = new StartGame(this, gameProcess),
                [typeof(EndGame)] = new EndGame(this, userInterfaceController),
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _currentState = _states[typeof(TState)];
            _currentState.Enter();
        }
    }

    public interface IState
    {
        public void Enter();
    }

    public class StartGame : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameProcess _gameProcess;
        
        public StartGame(GameStateMachine gameStateMachine, IGameProcess gameProcess)
        {
            _gameStateMachine = gameStateMachine;
            _gameProcess = gameProcess;
        }
        
        public void Enter()
        {
            _gameProcess.StartLevel(0);
            _gameProcess.EndLevels += OnEndLevels;
        }

        private void OnEndLevels()
        {
            _gameStateMachine.Enter<EndGame>();
        }
    }

    public class EndGame : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUserInterfaceController _userInterfaceController;
        
        public EndGame(GameStateMachine gameStateMachine, IUserInterfaceController userInterfaceController)
        {
            _gameStateMachine = gameStateMachine;
            _userInterfaceController = userInterfaceController;
        }
        
        public void Enter()
        {
            _userInterfaceController.ShowRestartView();
            _userInterfaceController.SelectedRestart = OnSelectedRestart;
        }
        
        private void OnSelectedRestart()
        {
            _userInterfaceController.HideRestartView();
            _gameStateMachine.Enter<StartGame>();
        }
    }
}