using RPG.Combat;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using RPG.Core;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health health;
        void Start()
        {
            // gets a reference to health script
            health = GetComponent<Health>();
        }
        void Update()
        {   
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
 
           // print("Nothing to do");
        }

        private bool InteractWithCombat()
        {

            //Sending out a ray from the ousepoitn and collecting all objects it hits
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            // loop through to see if any are combat targets to prioritise them
            foreach (RaycastHit hit in hits)
            {
                // find the component
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                // if its not there break this loop but keep looping through to the next iteration
                if (target == null) continue;

                // if there is a combat target, and it is not dead, continue
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                //check if the player is selecting that target
                if (Input.GetMouseButton(0))
                {
                    //attack the target
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {      
            //empty variable for the Raycasterhit info
            RaycastHit hit;

            // hit and collect data
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);        
            
            // if there is something
            if (hasHit)
            {
                //and the player clicks
                if (Input.GetMouseButton(0))
                {
                    //move the player
                    GetComponent<Mover>().StartMoveAction(hit.point,1f);
                }
            }
            return hasHit;
        }


        private static Ray GetMouseRay()
        {
            // function for sending a ray
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}