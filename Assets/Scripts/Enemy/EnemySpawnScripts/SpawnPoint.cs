using Assets.Scripts.Data.PlayerProgressFolder;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.StaticData.EnemyStaticData;
using UnityEngine;

namespace Assets.Scripts.Enemy.EnemySpawnScripts
{
    public class SpawnPoint : MonoBehaviour, ISavedProgressWriter
    {
        public string Id { get; set; }
        public MonsterTypeID monsterTypeID;
        public bool _slain;
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void UpdateProgress(PlayerProgress data)
        {
            if (_slain)
            {
                data.KillData.ClearedSpawners.Add(Id);
            }
        }

        public void LoadProgress(PlayerProgress data)
        {
            if (data.KillData.ClearedSpawners.Contains(Id))
                _slain = true;
            else
            {
                Spawn();
            }
        }

        private async void Spawn()
        {
            GameObject monster = await _gameFactory.CreateMonster(monsterTypeID, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.OnEnemyDeath += Slay;
        }

        private void Slay()
        {
            if (_enemyDeath != null)
                _enemyDeath.OnEnemyDeath -= Slay;
            _slain = true;
        }
    }
}