using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using Unity.Collections;
using UnityEngine.Analytics;


namespace RPG.Control
{
    [RequireComponent(typeof(Mover))]

    public class AIController : MonoBehaviour
    {
        [SerializeField] private float suspicionTime = 5;
        [SerializeField] private float wayPointTolerance = 1;
        [SerializeField] private float wayPointDelayTime = 3;
        [SerializeField] int chaseDistance = 10;
        [SerializeField] private PatrolPath patrolPath;
        [Range(0f, 1f)]
        [SerializeField] float patrolSpeedFraction = 0.2f;


        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private int currentWayPointIndex = 0;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;




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
            // guardPosition = this.transform.position;

            //get mover at the start
            mover = GetComponent<Mover>();

            // set guard position to start position
            guardPosition = this.transform.position;
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
                PatrolBehaviour();
            }

            UpdateTimers();

        }

        private void UpdateTimers()
        {
            //adding a timer to create suspicion
            timeSinceLastSawPlayer += Time.deltaTime;
            //adding timer to add delay at waypoint
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    CycleWaypoint();
                    timeSinceArrivedAtWaypoint = 0;
                }

                nextPosition = GetCurrentWayPoint();
            }

            if (timeSinceArrivedAtWaypoint > wayPointDelayTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }

        }

        private bool AtWayPoint()
        {
            float distanceToWaypoint = Vector3.Distance(this.transform.position, GetCurrentWayPoint());
            return distanceToWaypoint < wayPointTolerance;
        }
        private void CycleWaypoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWaypoint(currentWayPointIndex);
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
