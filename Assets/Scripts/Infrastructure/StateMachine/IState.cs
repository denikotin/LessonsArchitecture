using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}