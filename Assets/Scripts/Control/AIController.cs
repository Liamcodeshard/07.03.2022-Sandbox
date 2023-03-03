using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;


namespace RPG.Control
{        
    [RequireComponent(typeof(Mover))]

    public class AIController : MonoBehaviour
    {

        [SerializeField] int chaseDistance =10;
        [SerializeField] int strikeDistance = 2;
        GameObject player;
        Fighter fighter;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            if (IsInAttackRange(player) && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }

            // if (DistanceToPlayerCheck() < strikeDistance) GetComponent<Fighter>().Attack(player);
        }

        private bool IsInAttackRange(GameObject player)
        {

            return Vector3.Distance(this.transform.position, player.transform.position) < chaseDistance;
        }
    }
}
