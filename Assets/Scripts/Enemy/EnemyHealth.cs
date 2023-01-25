using Assets.Scripts.Logic;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public EnemyAnimator enemyAnimator;

        [SerializeField] private float _currentHP;
        [SerializeField] private float _maxHP;

        public float CurrentHP
        {
            get => _currentHP;
            set => _currentHP = value;
        }

        public float MaxHP
        {
            get => _maxHP;
            set => _maxHP = value;
        }

        public event Action OnHealthChanged;

        public void TakeDamage(float damage)
        {
            CurrentHP -= damage;
            enemyAnimator.PlayerHit();
            OnHealthChanged?.Invoke();
        }
    }
}