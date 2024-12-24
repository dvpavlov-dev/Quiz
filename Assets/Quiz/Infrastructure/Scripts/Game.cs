using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Quiz.Infrastructure
{
    public class Game : MonoBehaviour
    {
        [Inject]
        private void Constructor()
        {
            
        }
        
        private void Start()
        {
            GameStateMachine gameStateMachine = new GameStateMachine();
            gameStateMachine.Enter<StartGame>();
        }
    }

    public class GameStateMachine
    {
        private IState _currentState;

        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(StartGame)] = new StartGame(),
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
        public void Enter()
        {
            
        }
    }

    public class EndGame : IState
    {
        public void Enter()
        {
            
        }
    }
}