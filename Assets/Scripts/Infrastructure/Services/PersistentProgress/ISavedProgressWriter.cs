using Assets.Scripts.Data.PlayerProgressFolder;

namespace Assets.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressWriter : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress data);
    }
}