﻿using System;
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
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned =true;


        }

        private void SpawnPersistentObjects()
        {
            GameObject persistantObject = Instantiate(persistantObjectPrefab);
        }
    }
}
