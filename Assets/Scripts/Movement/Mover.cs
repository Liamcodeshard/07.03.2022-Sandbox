using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform targetObject;
        [SerializeField] GameObject camTarget;

        Ray lastRay;

        // Update is called once per frame
        void Update()
        {
            /*  if (Input.GetMouseButton(0)) //mouse button down is one click move
              {
                  MoveToCursor();
              }
      */        //    Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
            UpdateAnimator();
        }


        public void MoveTo(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity); // takes from global and converting into local //
            float speed = localVelocity.z; // we only care about the forward spoeed
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
   
        