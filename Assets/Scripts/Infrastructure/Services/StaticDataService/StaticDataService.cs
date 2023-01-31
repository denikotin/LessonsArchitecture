using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.StaticData.EnemyStaticData;
using Assets.Scripts.StaticData.LevelStaticDataFolder;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string MONSTER_PATH = "StaticData/Monsters";
        private const string LEVELS_PATH = "StaticData/Levels";
        private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;

        public void Load()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(MONSTER_PATH).ToDictionary(x => x.monsterID, x => x);

            _levels = Resources.LoadAll<LevelStaticData>(LEVELS_PATH).ToDictionary(x => x.LevelKey, x => x);
        }


        public MonsterStaticData ForMonster(MonsterTypeID typeID) =>
            _monsters.TryGetValue(typeID, out MonsterStaticData staticData) ? staticData : null;

        public LevelStaticData ForLevel(string sceneKey) => 
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;
    }
}
