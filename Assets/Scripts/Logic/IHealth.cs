using System;

namespace Assets.Scripts.Logic
{
    public interface IHealth
    {
        float CurrentHP { get; set; }
        float MaxHP { get; set; }

        event Action OnHealthChanged;

        void TakeDamage(float damage);
    }
}