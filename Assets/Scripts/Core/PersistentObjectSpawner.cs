using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistantObjectPrefab;

        private static bool hasSpawned = false;

        private void Awake()
        {
            //check if this object has already been created - if so then stop 
            if (hasSpawned) return;

            // if not, then spawn the prefab we have created in unity
            SpawnPersistentObjects();

            // if we spawned, then the global variable is set to true and so we wont have another spawned
            hasSpawned =true;


        }

        private void SpawnPersistentObjects()
        {
            // create the prefab in the game
            GameObject persistantObject = Instantiate(persistantObjectPrefab);
            // ensure it does not get destroyed across scenes
            DontDestroyOnLoad(persistantObject);
        }
    }
}
