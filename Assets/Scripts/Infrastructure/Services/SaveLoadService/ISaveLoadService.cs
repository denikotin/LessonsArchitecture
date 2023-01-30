using Assets.Scripts.Data.PlayerProgressFolder;
using Assets.Scripts.Infrastructure.Services;

namespace Assets.Scripts.Infrastructure.Services.SaveLoadService
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}
