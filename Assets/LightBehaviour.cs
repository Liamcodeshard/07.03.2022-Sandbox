using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
    public static bool lightsOut = false;

    void Update()
    {
        if (lightsOut)
        {
            TurnOffLights();
        }
        else
        {
            TurnOnLights();
        }
    }
    void TurnOffLights()
    {
        this.gameObject.SetActive(false);
        print("Liughtsoffff");
    }
    void TurnOnLights()
    {
        this.gameObject.SetActive(true);
    }
}
