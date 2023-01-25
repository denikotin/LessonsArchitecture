using Assets.Scripts.Logic;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAggression : MonoBehaviour
    {
        public TriggerObserver triggerObserver;
        public Follow follow;

        public float cooldown;
        private Coroutine _aggroRoutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            triggerObserver.OnTriggerEnterEvent += TriggerEnter;
            triggerObserver.OnTriggerExitEvent += TriggerExit;

            SwitchFollowOff();
        }

        private void TriggerEnter(Collider obj)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                StopAggroCoroutine();
                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                _aggroRoutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private void StopAggroCoroutine()
        {
            if (_aggroRoutine != null)
            {
                StopCoroutine(_aggroRoutine);
                _aggroRoutine = null;
            }
        }

        private void SwitchFollowOff()
        {
            follow.enabled = false;
        }

        private void SwitchFollowOn()
        {
            follow.enabled = true;
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(cooldown);
            SwitchFollowOff();
        }
    }

}
