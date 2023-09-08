using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using RPG.Control;
using CharacterController = RPG.Control.CharacterController;

public class CharacterAnimationTrigger : MonoBehaviour
{

    public GameObject simoneDeBeauvoir;
    public RPG.Control.CharacterController SimonesCharacterController;

    // Start is called before the first frame update
    void Start()
    {
        SimonesCharacterController = simoneDeBeauvoir.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && SimonesCharacterController.rescued)
        {
            SimonesCharacterController.SetCharacterDestination();
        }
    }
}
