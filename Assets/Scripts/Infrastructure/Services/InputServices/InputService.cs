using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.InputServices
{
    public abstract class InputService : IInputService
    {
        protected const string HORIZONTAL = "Horizontal";
        protected const string VERTICAL = "Vertical";
        protected const string BUTTON = "Fire";

        public abstract Vector2 Axis { get; }


        public bool IsAttackButtonUp() => SimpleInput.GetButtonUp(BUTTON);

        public Vector2 GetSimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(HORIZONTAL), SimpleInput.GetAxis(VERTICAL));
        }
    }
}

