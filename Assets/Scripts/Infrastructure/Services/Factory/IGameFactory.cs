using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject heroGameObject { get; }

        event Action HeroCreatedEvent;

        GameObject CreateHud();
        GameObject CreatePlayer(GameObject initialPoint);
        public void CleanUp();

    }
}