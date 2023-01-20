using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.InputServices
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => GetSimpleInputAxis();
    }

}

