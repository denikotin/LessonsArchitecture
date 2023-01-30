using Assets.Scripts.Data.PlayerProgressFolder;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Logic;
using Assets.Scripts.StaticData.EnemyStaticData;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour, ISavedProgressWriter
    {
        public MonsterTypeID monsterTypeID;
        private string _id;
        private IGameFactory _gameFactory;
        public bool _slain;
        private EnemyDeath _enemyDeath;

        private void Awake()
        {
            _id = GetComponent<UniqueID>().ID;
            _gameFactory = ServiceLocator.Container.Single<IGameFactory>();
        }

        public void UpdateProgress(PlayerProgress data)
        {
            if (_slain)
            {
                data.KillData.ClearedSpawners.Add(_id);
            }
        }

        public void LoadProgress(PlayerProgress data)
        {
            if (data.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            GameObject monster = _gameFactory.CreateMonster(monsterTypeID, transform);
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