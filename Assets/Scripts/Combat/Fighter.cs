using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using RPG.Movement;
using UnityEngine.Experimental.PlayerLoop;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 30f;


        [SerializeField] private float timeBetweenAttacks =.3f;
        
        private Health target;
        private Mover mover;
        private float timeSinceLastAttack =0;

        Animator animator;


        void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;

            if (!CanAttack(target.gameObject))
            {
                //here to stop the extra animation play when enemy is dead
                StopAttack();
                return;
            }

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }
        void AttackBehaviour()
        {
            transform.LookAt(target.transform.position);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("StopAttack");
            // this triggers the Hit() event
            animator.SetTrigger("Attack");
        }

        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;

            return target != null && !target.GetComponent<Health>().IsDead();
        }

        // animation event trioggered in punch at .9
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
            print("Attack");
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(b:target.transform.position, a:transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("StopAttack");
        }
    }
}
