using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace RPG.Core
{
    public class LightBehaviour : MonoBehaviour
    {
        public static bool lightsOut = false;

        void Update()
        {
            if (lightsOut)
            {
                TurnOffLights();
            }
            else if (!lightsOut) 
            {
                TurnOnLights();
            }
        }
        public void TurnOffLights()
        {
            this.gameObject.SetActive(false);
            print("Lightsoffff");
        }
        public void TurnOnLights()
        {
            this.gameObject.SetActive(true);
            print("Lightsonnn");
        }
    }

}
