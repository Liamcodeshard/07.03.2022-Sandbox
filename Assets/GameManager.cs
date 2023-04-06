using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] BuildingManager buildingManager;
    [SerializeField] LightBehaviour mainLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buildingManager != null && buildingManager.entered == true) 
        {
            mainLight.TurnOffLights();
        }
        else
        {
            mainLight.TurnOnLights();
        }
    }
}
