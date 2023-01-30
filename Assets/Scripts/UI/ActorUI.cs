using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HealthBar healthBar;
        private IHealth _health;


        public void Construct(IHealth health)
        {
            _health = health;
            _health.OnHealthChanged += UpdateHealthBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();
            if(health != null)
                Construct(health);
        }
        private void OnDestroy() => _health.OnHealthChanged -= UpdateHealthBar;

        private void UpdateHealthBar()
        {
            healthBar.SetValue(_health.CurrentHP, _health.MaxHP);
        }

    }
}
