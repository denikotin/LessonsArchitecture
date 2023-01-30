using Assets.Scripts.Infrastructure.Services.Factory;
using Assets.Scripts.Logic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        public EnemyAnimator enemyAnimator;
        public float attackCooldown;
        public float cleavage = 0.5f;
        public float damage = 10f;
        public float effectiveDistance = 0.5f;

        private Transform _heroTransform;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        public void Construct(Transform heroTransform) => _heroTransform = heroTransform;

        void Awake() => _layerMask = 1 << LayerMask.NameToLayer("Player");

        void Update()
        {
            UpdateCooldown();
            if (CanAttack())
            {
                StartAttack();
            }
        }

        private void OnAttack()
        {
            if(Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), cleavage, 1);
                hit.transform.GetComponent<IHealth>().TakeDamage(damage);
            }
        }
        private void OnAttackEnded()
        {
            _attackCooldown = attackCooldown;
            _isAttacking = false;
        }

        public void EnableAttack() => _attackIsActive = true;

        public void DisableAttack() => _attackIsActive = false;

        private bool Hit(out Collider hit)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            enemyAnimator.PlayAttack();
            _isAttacking = true;
        }
        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
            {
                _attackCooldown -= Time.deltaTime;
            }
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + (transform.forward * effectiveDistance);
        private bool CanAttack() => _attackIsActive &&!_isAttacking && CooldownIsUp();
        private bool CooldownIsUp() => _attackCooldown <= 0;

    }
}
