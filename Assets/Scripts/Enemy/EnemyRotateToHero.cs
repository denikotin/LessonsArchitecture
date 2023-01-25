using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.Factory;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyRotateToHero : Follow
    {
        public float speed;
        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            if (IsHeroExist())
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreatedEvent += InitializeHeroTransform;
            }
        }

        private bool IsHeroExist() =>
           _gameFactory.heroGameObject != null;
        

        private void InitializeHeroTransform() =>
            _heroTransform = _gameFactory.heroGameObject.transform;
        

        private void Update()
        {
            if (Initialized())
            {
                RotateTowardHero();
            }
        }

        private void RotateTowardHero()
        {
            UpdatePositionToLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }
        private void UpdatePositionToLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, positionDiff.y, positionDiff.z); 
        }

        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) => 
            Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 positionToLook) => 
            Quaternion.LookRotation(positionToLook);

        private float SpeedFactor() => speed * Time.deltaTime;

        private bool Initialized() => _heroTransform != null;
    }
}