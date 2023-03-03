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
        [SerializeField] int strikeDistance = 2;
        GameObject player;
        Fighter fighter;
        private Health health;

        // question for converting this script is: hoow do we get the target for the enemy as the player/
        void Start()
        {
            //gets this objects fighter script
            fighter = GetComponent<Fighter>();

            //gets a reference to the player
            player = GameObject.FindGameObjectWithTag("Player");

            // gets a reference to health script
            health = GetComponent<Health>();
        }

        void Update()
        {

            if (health.IsDead()) return;
            // fighting logic will already eb happening (checking if has target, checking distance +closing it)

            // then we check if the Player is close and not dead
            if (IsInAttackRange(player) && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }

        }

        private bool IsInAttackRange(GameObject player)
        {

            return Vector3.Distance(this.transform.position, player.transform.position) < chaseDistance;
        }
    }
}
