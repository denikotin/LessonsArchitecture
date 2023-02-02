using Assets.Scripts.StaticData.EnemyStaticData;
using Assets.Scripts.StaticData.LevelStaticDataFolder;
using Assets.Scripts.StaticData.WindowsStaticDataFolder;
using Assets.Scripts.UI.Services;

namespace Assets.Scripts.Infrastructure.Services.StaticDataService
{
    public interface IStaticDataService : IService
    {
        LevelStaticData ForLevel(string sceneKey);
        MonsterStaticData ForMonster(MonsterTypeID typeID);
        WindowConfig ForWindow(WindowID shop);
        void Load();
    }
}