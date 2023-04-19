using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using RPG.Movement;
using UnityEditor;
using UnityEngine.Experimental.PlayerLoop;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float weaponDamage = 30f;


        [SerializeField] private float timeBetweenAttacks =.3f;

        [SerializeField] private GameObject hitParticleSystem;
        
        private Health target;
        private Mover mover;
        private float timeSinceLastAttack =Mathf.Infinity;

        Animator animator;


        void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }
        void Update()
        {
            // counting from last attack to add a delay between attacks
            timeSinceLastAttack += Time.deltaTime;

            //initially no target for the fighter so null reference protection
            if (target == null) return;


            // see if the target is null or dead
            if (!CanAttack(target.gameObject))
            {
                //here to stop the extra animation play when enemy is dead
                StopAttack();
                return;
            }

            // if not in range, get in range
            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            //if in range, cancel movement and start attack
            else
            {
                mover.Cancel();

                //looks at target, checks when the last hit was, then triggers attach
                AttackBehaviour();
            }
        }
        void AttackBehaviour()
        {
            transform.LookAt(target.transform.position);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // sets off the trigger and resets the last one
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            // this triggers the Hit() event ***** ====
            animator.ResetTrigger("StopAttack");
            animator.SetTrigger("Attack");
        }

        public bool CanAttack(GameObject target)
        {
            if (target == null) return false;

            return target != null && !target.GetComponent<Health>().IsDead();
        }

        // animation event triggered in punch at .9
        void Hit()
        {
            if (target == null) return;
            // goes into health script of target and damages
            target.TakeDamage(weaponDamage);
            hitParticleSystem.SetActive(true);
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(b:target.transform.position, a:transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            // starting the action is enabling te update loop of the fighter
            // which searches for a target, once got it will move towards and attack
            GetComponent<ActionScheduler>().StartAction(this);

            //getting health point of the target for the Hit() event (which is triggered in the animation)
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
