using System;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.SaveLoadService;
using Assets.Scripts.Infrastructure.StateMachine.StateInterfaces;
using Assets.Scripts.Infrastructure.StateMachine.States;
using Assets.Scripts.Infrastructure.SceneLoaderFolder;
using Assets.Scripts.UI.Elements;
using Assets.Scripts.Infrastructure.Services.StaticDataService;
using Assets.Scripts.UI.Services.Factory;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, ServiceLocator services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),

                [typeof(LoadProgressState)] =
                    new LoadProgressState(this,
                                          services.Single<IPersistentProgressService>(),
                                          services.Single<ISaveLoadService>()),

                [typeof(LoadLevelState)] =
                    new LoadLevelState(this,
                                       sceneLoader,
                                       loadingCurtain,
                                       services.Single<IGameFactory>(),
                                       services.Single<IPersistentProgressService>(),
                                       services.Single<IStaticDataService>(),
                                       services.Single<IUIFactory>()),


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

