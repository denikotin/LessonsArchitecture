using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.StaticData.EnemyStaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string MONSTER_PATH = "StaticData/Monsters";
        private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(MONSTER_PATH).ToDictionary(x => x.monsterID, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeID typeID) =>
            _monsters.TryGetValue(typeID, out MonsterStaticData staticData) ? staticData : null;
    }
}
