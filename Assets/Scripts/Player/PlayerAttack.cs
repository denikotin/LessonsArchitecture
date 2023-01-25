using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.InputServices;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Logic;
using UnityEngine;


namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class PlayerAttack : MonoBehaviour, ISavedProgressReader
    {
        public HeroAnimator heroAnimator;
        public CharacterController characterController;
        private IInputService _inputService;

        private static int _layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");

        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp() && !heroAnimator.IsAttacking)
            {
                heroAnimator.PlayAttack();
            }
        }

        private void OnAttack()
        {
            for(int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.damage);
            }
        }

        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.damageRadius, _hits, _layerMask);
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, characterController.center.y / 2, transform.position.z);
        }

        public void LoadProgress(PlayerProgress data)
        {
            _stats = data.Stats;
        }
    }
}

