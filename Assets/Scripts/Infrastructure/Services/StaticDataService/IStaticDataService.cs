using Assets.Scripts.StaticData.EnemyStaticData;
using Assets.Scripts.StaticData.LevelStaticDataFolder;

namespace Assets.Scripts.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        LevelStaticData ForLevel(string sceneKey);
        MonsterStaticData ForMonster(MonsterTypeID typeID);
        void Load();
    }
}