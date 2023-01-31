using Assets.Scripts.Enemy;
using Assets.Scripts.Enemy.LootScripts;
using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.RandomService;
using Assets.Scripts.Logic;
using Assets.Scripts.StaticData.EnemyStaticData;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private IStaticDataService _staticData;
        private IRandomService _randomService;
        private IPersistentProgressService _progressService;

        public List<ISavedProgressReader> ProgressReaders { get; private set; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; private set; } = new List<ISavedProgressWriter>();
        public GameObject heroGameObject { get; set; }

        public GameFactory(IAssetProvider assets, IStaticDataService staticData, IRandomService randomService, IPersistentProgressService persistentProgress)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
            _progressService = persistentProgress;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            heroGameObject = InstantiateRegistered(AssetsPath.PLAYER_PATH, initialPoint.transform.position);
            return heroGameObject;
        }

        public void CreateSpawner(Vector3 position, string id, MonsterTypeID monsterTypeID)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetsPath.SPAWNER_PATH, position).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.Id = id;
            spawner.monsterTypeID = monsterTypeID;
        }

        public GameObject CreateMonster(MonsterTypeID typeID, Transform parent)
        {
            MonsterStaticData monsterStaticData = _staticData.ForMonster(typeID);
            GameObject monster = Object.Instantiate(monsterStaticData.monsterPrefab, parent.position, Quaternion.identity, parent);
            
            IHealth health = monster.GetComponent<IHealth>();
            health.CurrentHP = monsterStaticData.health;
            health.MaxHP = monsterStaticData.health;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<EnemyMoveToPlayer>()?.Construct(heroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterStaticData.moveSpeed;

            LootSpawner  lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterStaticData.MinLoot, monsterStaticData.MaxLoot);
            lootSpawner.Construct(this,_randomService);

            var attack = monster.GetComponent<EnemyAttack>();
            attack.Construct(heroGameObject.transform);
            attack.damage = monsterStaticData.damage;
            attack.cleavage = monsterStaticData.cleavage;
            attack.effectiveDistance = monsterStaticData.effectiveDistance;

            monster.GetComponent<EnemyRotateToHero>()?.Construct(heroGameObject.transform);

            return monster;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetsPath.LOOT_PATH).GetComponent<LootPiece>();
            lootPiece.Construct(_progressService.Progress.WorldData);
            return lootPiece;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetsPath.HUD_PATH);
            hud.GetComponentInChildren<LootCounter>().Constructor(_progressService.Progress.WorldData);
            return hud;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject instance = _assets.Instantiate(prefabPath, position);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject instance = _assets.Instantiate(prefabPath);
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