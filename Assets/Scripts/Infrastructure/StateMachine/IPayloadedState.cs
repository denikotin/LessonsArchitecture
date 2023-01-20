using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}