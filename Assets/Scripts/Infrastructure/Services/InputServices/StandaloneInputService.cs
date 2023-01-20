using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.InputServices
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = GetSimpleInputAxis();

                if (axis == Vector2.zero)
                {
                    axis = GetUnityAxis();
                }
                return axis;
            }
        }

        public Vector2 GetUnityAxis()
        {
            return new Vector2(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));
        }
    }
}

