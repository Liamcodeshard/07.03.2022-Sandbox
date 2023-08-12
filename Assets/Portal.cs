using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        public string sceneName;

        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                SceneManager.LoadScene(sceneName);
            }

        }
    }
    
}