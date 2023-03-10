using UnityEngine;
using Assets.Scripts.UI.Services.Windows;
using Assets.Scripts.UI.Services.Factory;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Ads;
using Assets.Scripts.Infrastructure.Services.InApp;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.SceneLoaderFolder;
using Assets.Scripts.Infrastructure.Services.RandomService;
using Assets.Scripts.Infrastructure.Services.InputServices;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.SaveLoadService;
using Assets.Scripts.Infrastructure.Services.StaticDataService;
using Assets.Scripts.Infrastructure.StateMachine.StateInterfaces;

namespace Assets.Scripts.Infrastructure.StateMachine.States
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
            IAdsService adsService = RegisterAdsService();
            IIAPService iAPService = RegisterInAppPurchaseService(new InAppProvider(), persistentProgress);

            _services.RegisterSingle(inputService);

            _services.RegisterSingle<IGameStateMachine>(_gameStateMachine);

            _services.RegisterSingle<IUIFactory>
                (new UIFactory(assetProvider, staticDataService,persistentProgress,adsService, iAPService));

            _services.RegisterSingle<IWindowService>
                (new WindowService(_services.Single<IUIFactory>()));

            _services.RegisterSingle<IGameFactory>
                (new GameFactory(assetProvider, staticDataService, randomService, persistentProgress, ServiceLocator.Container.Single<IWindowService>()));
            
            _services.RegisterSingle<ISaveLoadService>
                (new SaveLoadService(persistentProgress, ServiceLocator.Container.Single<IGameFactory>()));
            
            
        }

        private IStaticDataService RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
            return staticData;
        }

        private IIAPService RegisterInAppPurchaseService(InAppProvider iapProvider, IPersistentProgressService progressService)
        {
            IIAPService IAPService = new IAPService(iapProvider, progressService);
            IAPService.Initialize();
            _services.RegisterSingle<IIAPService>(IAPService);
            return IAPService;
        }

        private IAdsService RegisterAdsService()
        {
            IAdsService adsService = new AdsService();
            adsService.Initialize();
            _services.RegisterSingle<IAdsService>(adsService);
            return adsService;
        }

        private IRandomService RegisterRandomService()
        {
            IRandomService randomService = new RandomService();
            _services.RegisterSingle<IRandomService>(randomService);
            return randomService;
        }

        private IAssetProvider RegisterAssetsProvider()
        {
            IAssetProvider assetProvider = new AssetProvider();
            assetProvider.Initialize();
            _services.RegisterSingle<IAssetProvider>(assetProvider);
            return assetProvider;
        }
        private IPersistentProgressService RegisterPersistentProgressService()
        {
            IPersistentProgressService persistnentService = new PersistentProgressService();
            _services.RegisterSingle<IPersistentProgressService>(persistnentService);
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