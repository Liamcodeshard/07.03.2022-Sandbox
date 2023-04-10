using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngineInternal;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform targetObject;
        [SerializeField] GameObject camTarget;
        [SerializeField] float maxSpeed;

        private NavMeshAgent navMeshAgent;
        Animator animator;
        private Health health;

        Ray lastRay;

        void Start()
        {
            health = GetComponent<Health>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            
            UpdateAnimator();
        }
        
        // called in PlayerController interact with movement()
        public void StartMoveAction(Vector3 destination)
        {
            // starts the update loop which updates animator based off the speed off the navmesh
            GetComponent<ActionScheduler>().StartAction(this);

            //starts the movement
            MoveTo(destination);
        }

        // called in PlayerController interact with movement()
        // overload made for enemies to have different speeds for patrolling and chasing
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            // starts the update loop which updates animator based off the speed off the navmesh
            GetComponent<ActionScheduler>().StartAction(this);

            //starts the movement
            MoveTo(destination, speedFraction);
        }
       
        
        public void MoveTo(Vector3 destination)
        {
            // stops the fighter script dead to prioritise movement
            animator.SetTrigger("StopAttack");
            //sets destination
            navMeshAgent.destination = destination;
            // negates any previous order to stop
            navMeshAgent.isStopped = false;

        }       

        // this overload made for enemies to have different speeds for patrolling and chasing
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            // stops the fighter script dead to prioritise movement
            animator.SetTrigger("StopAttack");
            //sets destination
            navMeshAgent.destination = destination;
            // set navmesh agent speed
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            // negates any previous order to stop
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            // stops any movement
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            //gets speed from navmesh
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            // takes from global and converting into local
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            // we only care about the forward speed
            float speed = localVelocity.z;
            //settong the speed of the walk animation
            animator.SetFloat("forwardSpeed", speed);
        }
    }
}
   
        