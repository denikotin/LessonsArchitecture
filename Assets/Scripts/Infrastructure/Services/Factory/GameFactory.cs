using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.Logic;
using Assets.Scripts.Enemy;
using System.Threading.Tasks;
using Assets.Scripts.UI.Elements;
using System.Collections.Generic;
using Assets.Scripts.Enemy.LootScripts;
using Assets.Scripts.UI.Services.Windows;
using Assets.Scripts.Enemy.EnemySpawnScripts;
using Assets.Scripts.StaticData.EnemyStaticData;
using Assets.Scripts.Infrastructure.Services.RandomService;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.StaticDataService;

namespace Assets.Scripts.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private IStaticDataService _staticData;
        private IRandomService _randomService;
        private IPersistentProgressService _progressService;
        private IWindowService _windowService;

        public List<ISavedProgressReader> ProgressReaders { get; private set; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; private set; } = new List<ISavedProgressWriter>();
        public GameObject heroGameObject { get; set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticData, IRandomService randomService, IPersistentProgressService persistentProgress, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
            _progressService = persistentProgress;
            _windowService = windowService;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assets.CleanUp();
        }

        public async Task WarmUp()
        {
           await _assets.Load<GameObject>(AssetsAddress.SPAWNER_ADDRESS);
           await _assets.Load<GameObject>(AssetsAddress.LOOT_ADDRESS);
        }



        public async Task CreateSpawner(Vector3 position, string id, MonsterTypeID monsterTypeID)
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetsAddress.SPAWNER_ADDRESS);
            SpawnPoint spawner = InstantiateRegistered(prefab, position).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.Id = id;
            spawner.monsterTypeID = monsterTypeID;
        }

        public async Task<GameObject> CreateMonster(MonsterTypeID typeID, Transform parent)
        {
            MonsterStaticData monsterStaticData = _staticData.ForMonster(typeID);

            GameObject prefab = await _assets.Load<GameObject>(monsterStaticData.prefabReference);
            GameObject monster = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
            
            IHealth health = monster.GetComponent<IHealth>();
            health.CurrentHP = monsterStaticData.health;
            health.MaxHP = monsterStaticData.health;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<EnemyMoveToPlayer>()?.Construct(heroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterStaticData.moveSpeed;

            LootSpawner  lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterStaticData.MinLoot, monsterStaticData.MaxLoot);
            lootSpawner.Construct(this,_randomService);

            EnemyAttack attack = monster.GetComponent<EnemyAttack>();
            attack.Construct(heroGameObject.transform);
            attack.damage = monsterStaticData.damage;
            attack.cleavage = monsterStaticData.cleavage;
            attack.effectiveDistance = monsterStaticData.effectiveDistance;

            monster.GetComponent<EnemyRotateToHero>()?.Construct(heroGameObject.transform);

            return monster;
        }

        public async Task<LootPiece> CreateLoot()
        {
            GameObject prefab = await _assets.Load<GameObject>(AssetsAddress.LOOT_ADDRESS);
            LootPiece lootPiece = InstantiateRegistered(prefab).GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.Progress.WorldData);
            return lootPiece;
        }

        public async Task<GameObject> CreatePlayer(Vector3 initialPoint)
        {
            heroGameObject = await InstantiateRegisteredAsync(AssetsAddress.PLAYER_PATH, initialPoint);
            return heroGameObject;
        }

        public async Task<GameObject> CreateHud()
        {
            GameObject hud = await InstantiateRegisteredAsync(AssetsAddress.HUD_PATH);
            hud.GetComponentInChildren<LootCounter>().Constructor(_progressService.Progress.WorldData);

            foreach(OpenWindowButton button in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                button.Construct(_windowService);
            }

            return hud;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 position)
        {
            GameObject instance = await _assets.InstantiateAsync(prefabPath, position);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject instance = await _assets.InstantiateAsync(prefabPath);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 position)
        {
            GameObject instance = Object.Instantiate(prefab, position, Quaternion.identity);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject instance = Object.Instantiate(prefab);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private void RegisterProgressWatchers(GameObject instance)
        {
            foreach (ISavedProgressReader progressReader in instance.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgressWriter progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }
            ProgressReaders.Add(progressReader);
        }

    }
}