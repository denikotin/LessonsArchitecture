using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.Infrastructure.Services.SaveLoadService
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}
