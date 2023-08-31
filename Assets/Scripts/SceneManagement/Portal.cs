using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
        }
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] string sceneName;
        [SerializeField] Transform spawnPoint;
        [SerializeField] CanvasGroup faderCamvas;
        GameObject player;

        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }

        }
        IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            // faderCamvas.alpha = 1.0f;
            yield return SceneManager.LoadSceneAsync(sceneName);
        
            print("sceneloaded");

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

  //          faderCamvas.alpha = 0f;  Replace with FadeOit();

            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {

            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if(portal == this) continue;
                if (portal.destination == destination) return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.transform.position);
          //  player.transform.position = otherPortal.spawnPoint.transform.position;
          //  player.transform.rotation = otherPortal.spawnPoint.transform.rotation;

        }

    }
    
}