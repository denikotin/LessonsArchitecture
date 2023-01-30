using Assets.Scripts.Data.PlayerProgressFolder;

namespace Assets.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress data);
    }
}