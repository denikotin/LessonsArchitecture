using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.InputServices;
using Assets.Scripts.Infrastructure.Services.RandomService;
using Assets.Scripts.Infrastructure.Services.SaveLoadService;
using Assets.Scripts.StaticData;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string INITIAL_SCENE_NAME = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices();
        }

        public void Enter() => _sceneLoader.Load(INITIAL_SCENE_NAME, onLoaded: EnterLoadProgressState);

        public void Exit()
        {
           
        }

        private void RegisterServices()
        {
            IInputService inputService = InputService();
            IStaticDataService staticDataService = RegisterStaticData();
            IRandomService randomService = RegisterRandomService();
            IAssetProvider assetProvider = RegisterAssetsProvider();
            IPersistentProgressService persistentProgress = RegisterPersistentProgressService();

            _services.RegisterSingle(inputService);
            _services.RegisterSingle<IGameFactory>(new GameFactory(assetProvider, staticDataService, randomService, persistentProgress));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(persistentProgress, ServiceLocator.Container.Single<IGameFactory>()));

        }

        private IStaticDataService RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            _services.RegisterSingle(staticData);
            return staticData;
        }

        private IRandomService RegisterRandomService()
        {
            IRandomService randomService = new RandomService();
            _services.RegisterSingle(randomService);
            return randomService;
        }

        private IAssetProvider RegisterAssetsProvider()
        {
            IAssetProvider assetProvider = new AssetProvider();
            _services.RegisterSingle(assetProvider);
            return assetProvider;
        }
        private IPersistentProgressService RegisterPersistentProgressService()
        {
            IPersistentProgressService persistnentService = new PersistentProgressService();
            _services.RegisterSingle(persistnentService);
            return persistnentService;
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