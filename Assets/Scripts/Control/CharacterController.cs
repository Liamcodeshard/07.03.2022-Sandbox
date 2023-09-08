using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace RPG.Control
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] Transform destination;
        public bool rescued;
        public bool toldStory;

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
        }

        public void SetCharacterDestination()
        {
            mover.StartMoveAction(destination.position);
            animator.SetBool("toldStory", true);
        }
    }
}
