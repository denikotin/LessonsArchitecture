using Assets.Scripts.Data;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress data);
    }
}