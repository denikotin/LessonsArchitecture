using Assets.Scripts.StaticData.EnemyStaticData;

namespace Assets.Scripts.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeID typeID);
        void LoadMonsters();
    }
}