using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth enemyHealth;
        public EnemyAnimator enemyAnimator;

        public GameObject deathFX;
        public event Action OnEnemyDeath;

        private void Start()
        {
            enemyHealth.OnHealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            enemyHealth.OnHealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (enemyHealth.CurrentHP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            enemyHealth.OnHealthChanged -= HealthChanged;
            enemyAnimator.PlayDeath();
            SpawnDeathFx();
            StartCoroutine(DestroyTimer());
            OnEnemyDeath?.Invoke();
        }

        private void SpawnDeathFx()
        {
            Instantiate(deathFX, transform.position, Quaternion.identity);
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }

    }
}