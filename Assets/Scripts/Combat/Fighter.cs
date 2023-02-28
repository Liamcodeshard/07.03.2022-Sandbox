using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using UnityEngine.Experimental.PlayerLoop;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float weaponRange = 2f;
        private Transform target;
        private Mover mover;


        void Start()
        {
            mover = GetComponent<Mover>();
        }
        void Update()
        {
            if (target == null) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Stop();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(b:target.position, a:transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
           target = combatTarget.transform;
           print("ATTAACCKK");
        }

        public void Cancel()
        {
            print("Cancelled");
            target = null;
        }
    }
}
