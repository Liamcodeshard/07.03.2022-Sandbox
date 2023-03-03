using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;


namespace RPG.Control
{        
    [RequireComponent(typeof(Mover))]

    public class AIController : MonoBehaviour
    {

        [SerializeField] int chaseDistance =10;
        GameObject player;
        Fighter fighter;
        private Health health;
        private Mover mover;
        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float suspicionTime = 5;


        private Vector3 playersLastSeenAtPosition;

        // question for converting this script is: hoow do we get the target for the enemy as the player/
        void Start()
        {
            //gets this objects fighter script
            fighter = GetComponent<Fighter>();

            //gets a reference to the player
            player = GameObject.FindGameObjectWithTag("Player");

            // gets a reference to health script
            health = GetComponent<Health>();

            // get location at start of game
            guardPosition = this.transform.position;

            //get mover at the start
            mover = GetComponent<Mover>();
        }

        void Update()
        {


            if (health.IsDead()) return;
            // fighting logic will already eb happening (checking if has target, checking distance +closing it)

            // then we check if the Player is close and not dead
            if (IsInAttackRange(player) && fighter.CanAttack(player))
            {                
                AttackBehaviour();
                //when we see the player we start the timer at 0
                timeSinceLastSawPlayer = 0;

            }
            //suspicion logic
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            //adding a timer to create suspicion
            timeSinceLastSawPlayer += Time.deltaTime;

        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool IsInAttackRange(GameObject player)
        {

            return Vector3.Distance(this.transform.position, player.transform.position) < chaseDistance;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(this.transform.position, chaseDistance);
        }
    }
}
