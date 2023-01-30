using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    public class EnemyMoveToPlayer : Follow
    {
        public NavMeshAgent agent;
        private Transform _heroTransform;

        public void Construct(Transform heroGameObject)
        {
            _heroTransform = heroGameObject;
        }

        private void Update()
        {
            SetDestinationForAgent();
        }

        private void SetDestinationForAgent()
        {
            if (_heroTransform)
                agent.destination = _heroTransform.position;
        }
    }
    
}

