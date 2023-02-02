using Assets.Scripts.StaticData.EnemyStaticData;
using Assets.Scripts.StaticData.LevelStaticDataFolder;
using Assets.Scripts.StaticData.WindowsStaticDataFolder;
using Assets.Scripts.UI.Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        private const string MONSTER_PATH = "StaticData/Monsters";
        private const string LEVELS_PATH = "StaticData/Levels";
        private const string WINDOW_PATH = "StaticData/Windows/WindowData";
        private Dictionary<MonsterTypeID, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowID, WindowConfig> _windowConfigs;

        public void Load()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(MONSTER_PATH).ToDictionary(x => x.monsterID, x => x);

            _levels = Resources.LoadAll<LevelStaticData>(LEVELS_PATH).ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs = Resources.Load<WindowsStaticData>(WINDOW_PATH)
                .Configs
                .ToDictionary(x => x.WindowID, x => x);
        }


        public MonsterStaticData ForMonster(MonsterTypeID typeID) =>
            _monsters.TryGetValue(typeID, out MonsterStaticData staticData) ? staticData : null;

        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;

        public WindowConfig ForWindow(WindowID windowId) =>
            _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig) ? windowConfig : null;
    }
}
