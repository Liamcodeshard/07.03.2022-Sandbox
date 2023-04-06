using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
    //public static bool lightsOut = false;

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
