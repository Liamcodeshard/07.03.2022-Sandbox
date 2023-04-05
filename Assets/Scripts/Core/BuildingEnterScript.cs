using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class BuildingEnterScript : MonoBehaviour
{
    bool inside = false;
    [SerializeField] GameObject inner, outer;
    Vector3 centrePoint;
    GameObject player;
    [SerializeField] float buildingWidth;

    bool justLeft = false;
    void Start()
    {
        centrePoint = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        OnBuildingExit();
    }

    void Update()
    {
        IsInRange();
        if (inside)
        {
            OnBuildingEnter();
        }
        else if(justLeft && !inside)
        {
            OnBuildingExit();
        }
    }

    private void IsInRange()
    {
        inside = Vector3.Distance(centrePoint, player.transform.position) < buildingWidth;
    }

    public void OnBuildingEnter()
    {
        inner.SetActive(true);
        outer.SetActive(false);
        LightBehaviour.lightsOut = true;
        justLeft = true;
        print("Entered");
    }
    public void OnBuildingExit()
    {
        inner.SetActive(false);
        outer.SetActive(true);
        LightBehaviour.lightsOut = false;
        justLeft = false;
        print("Exited");
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, buildingWidth);
    }
}
