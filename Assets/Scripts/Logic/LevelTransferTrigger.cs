using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.StateMachine;
using Assets.Scripts.Infrastructure.StateMachine.States;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";
        public string transferTo;
        private IGameStateMachine _stateMachine;

        private bool _triggered;

        public BoxCollider Collider;

        private void Awake()
        {
            //УБРАТЬ SERVICELOCATOR
            _stateMachine = ServiceLocator.Container.Single<IGameStateMachine>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_triggered)
                return;

            if(other.CompareTag(PLAYER_TAG))
            {
                _stateMachine.Enter<LoadLevelState, string>(transferTo);
                _triggered = true;  
            }
                
        }

        private void OnDrawGizmos()
        {
            if (!Collider)
                return;

            Gizmos.color = new Color32(120, 200, 150, 130);
            Gizmos.DrawCube(transform.position + Collider.center, Collider.size);

        }
    }
}