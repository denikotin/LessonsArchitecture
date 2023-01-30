using Assets.Scripts.Data;
using Assets.Scripts.Data.PlayerProgressFolder;
using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.SaveLoadService
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PROGRESS = "Progress";
        private readonly IPersistentProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(PROGRESS)?.ToDeserialized<PlayerProgress>();
        }

        public void SaveProgress()
        {
            foreach (ISavedProgressWriter progressWriters in _gameFactory.ProgressWriters)
            {
                progressWriters.UpdateProgress(_progressService.Progress);
            }

            PlayerPrefs.SetString(PROGRESS, _progressService.Progress.ToJson());
        }
    }
}