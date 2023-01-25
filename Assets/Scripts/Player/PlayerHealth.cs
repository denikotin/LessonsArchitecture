using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Logic;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(HeroAnimator))]
    public class PlayerHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public HeroAnimator animator;
        public event Action OnHealthChanged;

        private State _state;

        public float CurrentHP
        {
            get => _state.currentHealth;
            set
            {
                if (_state.currentHealth != value)
                {
                    _state.currentHealth = value;
                    OnHealthChanged?.Invoke();
                }
            }
        }
        public float MaxHP
        {
            get => _state.maxHealth;
            set => _state.maxHealth = value;
        }

        public void LoadProgress(PlayerProgress data)
        {
            _state = data.HeroState;
            OnHealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress data)
        {
            data.HeroState.currentHealth = CurrentHP;
            data.HeroState.maxHealth = MaxHP;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHP <= 0)
            {
                return;
            }

            CurrentHP -= damage;
            animator.PlayHit();
        }

    }
}