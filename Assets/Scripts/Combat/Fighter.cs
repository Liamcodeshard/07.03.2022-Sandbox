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
        [SerializeField] private float weaponStrikeDelay = .45f;


        [SerializeField] private float timeBetweenAttacks =.3f;
        
        private Transform target;
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

            if (!GetIsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }
        void AttackBehaviour()
        {

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // this triggers the Hit() event
                animator.SetTrigger("Attack");
                timeSinceLastAttack = 0;
             
            }
        }

        // animation event trioggered in punch at .9
        void Hit()
        {
            Health healthComp = target.GetComponent<Health>();
            healthComp.TakeDamage(weaponDamage);
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(b:target.position, a:transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);

            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

    }
}
