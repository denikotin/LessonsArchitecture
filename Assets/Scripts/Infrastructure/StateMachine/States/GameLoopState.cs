using Assets.Scripts.Infrastructure.StateMachine.StateInterfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }

    }
}