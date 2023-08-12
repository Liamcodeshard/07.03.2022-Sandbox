using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Cinematic
{

    //This goes on the enemy to defeat - when he is dead this cutscene sctivates

    public class MaryCutSceneTrigger : MonoBehaviour
    {
        public GameObject Mary;

        RPG.Control.CharacterController controller;

        Health health;

        void Start()
        {
            Mary = GameObject.FindGameObjectWithTag("Mary");
            controller = Mary.GetComponent<RPG.Control.CharacterController>();
            health = this.GetComponent<Health>();
        }

        void Update()
        {
            if (health.IsDead())
            {
                controller.rescued = true;
            }
        }
    }
}
