using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    public class EnemyMoveToPlayer : Follow
    {
        private const float minimalDistance = 1f;
        public NavMeshAgent agent;
        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            if (_gameFactory.heroGameObject != null)
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreatedEvent += HeroCreated;
            }
        }
        private void Update()
        {
            if (Initialized() && HeroNotReached())
                agent.destination = _heroTransform.position;
        }

        private void HeroCreated()
        {
            InitializeHeroTransform();
        }

        private void InitializeHeroTransform()
        {
            _heroTransform = _gameFactory.heroGameObject.transform;
        }

        private bool Initialized()
        {
            return _heroTransform != null;
        }

        private bool HeroNotReached() => Vector3.Distance(agent.transform.position, _heroTransform.position) >= minimalDistance;

    }
    
}

