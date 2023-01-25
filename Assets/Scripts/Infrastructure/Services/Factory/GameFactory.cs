using Assets.Scripts.Infrastructure.Services.AssetManagement;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        public event Action HeroCreatedEvent;

        public List<ISavedProgressReader> ProgressReaders { get; private set; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; private set; } = new List<ISavedProgress>();

        public GameObject heroGameObject { get; set; }

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            heroGameObject = InstantiateRegistered(AssetsPath.PLAYER_PATH, initialPoint.transform.position);
            HeroCreatedEvent?.Invoke();
            return heroGameObject;
        }
        public GameObject CreateHud() =>
            InstantiateRegistered(AssetsPath.HUD_PATH);

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject instance = _assets.Instantiate(prefabPath, position);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject instance = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }
            ProgressReaders.Add(progressReader);
        }
        private void RegisterProgressWatchers(GameObject instance)
        {
            foreach (ISavedProgressReader progressReader in instance.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

    }
}