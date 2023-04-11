using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Control
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] Transform destination;
        [SerializeField] bool rescued;
        [SerializeField] bool toldStory;

        Animator animator;
        Mover mover;


        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if(rescued)
            {
                animator.SetBool("rescued", true);
            }
            if(toldStory)
            {
                SetCharacterDestination(destination);
            }
        }

        private void SetCharacterDestination(Transform _destination)
        {
            mover.StartMoveAction(destination.position);
            animator.SetBool("toldStory", true);
        }
    }
}
