using Assets.Scripts.StaticData.EnemyStaticData;
using System;
using UnityEngine;

namespace Assets.Scripts.StaticData.LevelStaticDataFolder
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string id;
        public MonsterTypeID MonsterTypeID;
        public Vector3 position;

        public EnemySpawnerData(string id, MonsterTypeID monsterTypeID, Vector3 position)
        {
            this.id = id;
            MonsterTypeID = monsterTypeID;
            this.position = position;
        }
    }
}
