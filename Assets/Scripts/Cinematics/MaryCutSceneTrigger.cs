using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Cinematic
{

    public class MaryCutSceneTrigger : MonoBehaviour
    {
        public GameObject Mary;

        RPG.Control.CharacterController controller;

        Health health;

        void Start()
        {
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
