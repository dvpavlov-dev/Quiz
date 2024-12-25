using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Quiz.Infrastructure
{
    public class Game : MonoBehaviour
    {
        private IGameProcess _gameProcess;
        
        [Inject]
        private void Constructor(IGameProcess gameProcess)
        {
            _gameProcess = gameProcess;

        }
        
        private void Start()
        {
            GameStateMachine gameStateMachine = new GameStateMachine(_gameProcess);
            gameStateMachine.Enter<StartGame>();
        }
    }

    public class GameStateMachine
    {
        private IState _currentState;

        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine(IGameProcess gameProcess)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(StartGame)] = new StartGame(this, gameProcess),
                [typeof(EndGame)] = new EndGame(),
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
            _gameProcess.StartLevel();
            _gameProcess.EndLevels += OnEndLevels;
        }

        private void OnEndLevels()
        {
            _gameStateMachine.Enter<EndGame>();
        }
    }

    public class EndGame : IState
    {
        public void Enter()
        {
            
        }
    }
}