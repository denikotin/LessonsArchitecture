using Assets.Scripts.Enemy.LootScripts;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.StaticData.EnemyStaticData;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }

        public void CleanUp();
        Task CreateSpawner(Vector3 position, string spawnerId, MonsterTypeID monsterTypeID);
        Task<GameObject> CreateHud();
        Task<LootPiece> CreateLoot();
        Task<GameObject> CreatePlayer(Vector3 initialPoint);
        Task WarmUp();
        Task<GameObject> CreateMonster(MonsterTypeID monsterTypeID, Transform parent);
    }
}