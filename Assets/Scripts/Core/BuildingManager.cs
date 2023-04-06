using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

namespace RPG.Core
{

    public class BuildingManager : MonoBehaviour
    {
        BuildingEnterScript[] buildings;
        public bool entered;


        // Start is called before the first frame update
        void Start()
        {
            buildings = GetComponentsInChildren<BuildingEnterScript>();
            print(buildings.Length);
        }
        // Update is called once per frame
        void Update()
        {
            entered = AnyBuildingEntered();

        }

        private bool AnyBuildingEntered()
        {
            for (int i = 0; i < buildings.Length; i++)
            {
                if (buildings[i].inside == true)
                {
                    print(buildings[i].inside == true);
                    return true;
                }
            }
            return false;
        }
    }

}