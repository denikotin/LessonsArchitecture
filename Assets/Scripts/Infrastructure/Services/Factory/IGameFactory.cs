using Assets.Scripts.Enemy.LootScripts;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.StaticData.EnemyStaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgressWriter> ProgressWriters { get; }

        public void CleanUp();
        void CreateSpawner(Vector3 position, string spawnerId, MonsterTypeID monsterTypeID);
        GameObject CreateHud();
        LootPiece CreateLoot();
        GameObject CreatePlayer(GameObject initialPoint);
        GameObject CreateMonster(MonsterTypeID monsterTypeID, Transform parent);
    }
}