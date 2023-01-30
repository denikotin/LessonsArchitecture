using Assets.Scripts.Data;
using Assets.Scripts.Data.LootData;
using Assets.Scripts.Data.PlayerProgressFolder;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.RandomService;
using Assets.Scripts.Logic;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Enemy.LootScripts
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath enemyDeath;
        private IGameFactory _factory;
        private IRandomService _randomService;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory factory, IRandomService random)
        {
            _factory = factory;
            _randomService = random;
        }

        private void Start()
        {
            enemyDeath.OnEnemyDeath += SpawnLoot;
        }

        private void OnDestroy()
        {
            enemyDeath.OnEnemyDeath -= SpawnLoot;
        }

        private void SpawnLoot()
        {
            LootPiece loot = _factory.CreateLoot();
            loot.transform.position = transform.position;
            string uniqueID = loot.GetComponent<UniqueID>().ID;
            Loot lootItem = GenerateLoot(uniqueID, loot.transform.position);

            loot.Initialize(lootItem);
        }

        private Loot GenerateLoot(string uniqueID, Vector3 lootPosition)
        {
            return new Loot()
            {
                value = _randomService.Next(_lootMin, _lootMax),
            };
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

    }
}