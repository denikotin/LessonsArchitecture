using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.InputServices
{
    public interface IInputService : IService
    {
        public Vector2 Axis { get; }

        public bool IsAttackButtonUp();
    }
}


