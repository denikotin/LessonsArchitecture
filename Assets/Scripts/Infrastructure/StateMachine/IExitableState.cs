using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.StateMachine
{
    public interface IExitableState
    {
        public void Exit();
    }
}