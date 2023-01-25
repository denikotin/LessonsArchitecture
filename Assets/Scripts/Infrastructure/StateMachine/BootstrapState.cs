using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.InputServices;
using Assets.Scripts.Infrastructure.Services.SaveLoadService;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string INITIAL_SCENE_NAME = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(INITIAL_SCENE_NAME, onLoaded: EnterLoadProgressState);
        }

        public void Exit()
        {
           
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(AllServices.Container.Single<IPersistentProgressService>(), AllServices.Container.Single<IGameFactory>()));
        }
        private IInputService InputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();

            }
            else
            {
                return new MobileInputService();
            }
        }

        private void EnterLoadProgressState() => _gameStateMachine.Enter<LoadProgressState>();

    }
}