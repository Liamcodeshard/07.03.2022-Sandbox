using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{

    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        [SerializeField] private Animator animator;
        private bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            // ensures any damage cannot take health below 100
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            // triggers death if health is at 0
            if (healthPoints ==0) TriggerDeath();
        }

        public void TriggerDeath()
        {
            // checks the player is not ALREADY dead
            if (isDead) return;
            // sets off animation
            animator.SetTrigger("Dead");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            isDead = true;
        }

    }
}
