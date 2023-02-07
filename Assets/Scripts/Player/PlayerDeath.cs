using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public PlayerMove playerMove;
        public HeroAnimator playerAnimator;
        public PlayerAttack playerAttack;

        public GameObject deathFx;
        private bool _isDead;

        private void Start() => playerHealth.OnHealthChanged += HealthChanged;

        private void OnDestroy() => playerHealth.OnHealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && playerHealth.CurrentHP <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            playerMove.enabled = false;
            playerAttack.enabled = false;
            playerAnimator.PlayDeath();
            Instantiate(deathFx, transform.position, Quaternion.identity);
        }
    }
}