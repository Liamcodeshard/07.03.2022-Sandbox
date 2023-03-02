using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat{

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
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints ==0) TriggerDeath();
        }

        public void TriggerDeath()
        {
            if (isDead) return;
            animator.SetTrigger("Dead");
            isDead = true;
        }

    }
}
