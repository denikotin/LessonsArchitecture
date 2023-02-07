using UnityEngine;
using Assets.Scripts.Player;
using System.Threading.Tasks;
using Assets.Scripts.UI.Elements;
using UnityEngine.SceneManagement;
using Assets.Scripts.Logic.CameraLogic;
using Assets.Scripts.UI.Services.Factory;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.SceneLoaderFolder;
using Assets.Scripts.StaticData.LevelStaticDataFolder;
using Assets.Scripts.Infrastructure.Services.StaticDataService;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.StateMachine.StateInterfaces;

namespace Assets.Scripts.Infrastructure.StateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine gameStateMachine,
                              SceneLoader sceneLoader, 
                              LoadingCurtain loadingCurtain, 
                              IGameFactory gameFactory, 
                              IPersistentProgressService progressService, 
                              IStaticDataService staticData,
                              IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticData;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.CleanUp();
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => _loadingCurtain.Hide();

        private async void OnLoaded()
        {
            await InitUIRoot();
            await InitializeGameWorld();
            InformProgressReader();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private async Task InitializeGameWorld()
        {
            LevelStaticData levelData = LoadLevelStaticData();
            await InitSpawners(levelData);
            GameObject player = await _gameFactory.CreatePlayer(levelData.InitialPlayerPosition);
            await InitHud(player);
            CameraFollow(player);
        }

        private LevelStaticData LoadLevelStaticData()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticDataService.ForLevel(sceneKey);
            return levelData;
        }

        private async Task InitSpawners(LevelStaticData levelData)
        {
            foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners)
            {
               await _gameFactory.CreateSpawner(spawnerData.position, spawnerData.id, spawnerData.MonsterTypeID);
            }
        }

        private async Task InitHud(GameObject player)
        {
            GameObject hud = await _gameFactory.CreateHud();
            hud.GetComponentInChildren<ActorUI>()
                    .Construct(player.GetComponent<PlayerHealth>());
        }

        private async Task InitUIRoot() => 
            await _uiFactory.CreateUIRoot();

        private void InformProgressReader()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }
        private void CameraFollow(GameObject player) => 
            Camera.main.GetComponent<CameraFollow>().SetFollowing(player);
    }
}