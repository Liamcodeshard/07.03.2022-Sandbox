using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Control
{        
    [RequireComponent(typeof(Mover))]

    public class AIController : MonoBehaviour
    {

        [SerializeField] int chaseDistance;
        GameObject player;

       void Update()
       {
            player = GameObject.FindGameObjectWithTag("Player");
            if(Vector3.Distance(this.transform.position, player.transform.position) < chaseDistance)
            {
                print("Chase");
                this.transform.LookAt(player.transform.position);
                this.GetComponent<Mover>().MoveTo(player.transform.position);
                
            }
       }
    }
}
