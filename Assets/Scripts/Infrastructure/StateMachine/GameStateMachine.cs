using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.SaveLoadService;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;


        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, ServiceLocator services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                
                [typeof(LoadProgressState)] = 
                    new LoadProgressState(this, sceneLoader, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
                
                [typeof(LoadLevelState)] = 
                    new LoadLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(), services.Single<IPersistentProgressService>()),
                
                
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}

