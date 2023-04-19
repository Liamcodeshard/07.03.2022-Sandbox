using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{

    public class CinematicTrigger : MonoBehaviour
    {
        PlayableDirector PD;
        bool triggered = false;

        void Start()
        {
            PD = GetComponent<PlayableDirector>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !triggered)
            {
                PD.Play();
                triggered = true;
            }
        }
    }
}
