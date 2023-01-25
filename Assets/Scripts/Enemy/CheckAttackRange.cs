using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        public EnemyAttack attack;
        public TriggerObserver triggerObserver;

        private void Start()
        {
            triggerObserver.OnTriggerEnterEvent += TriggerEnter;
            triggerObserver.OnTriggerExitEvent += TriggerExit;

            attack.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            attack.EnableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            attack.DisableAttack();
        }
    }
}