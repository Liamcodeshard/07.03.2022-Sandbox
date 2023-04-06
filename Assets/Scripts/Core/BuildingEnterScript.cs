using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class BuildingEnterScript : MonoBehaviour
{
    public bool inside = false;
    [SerializeField] GameObject inner, outer;
    Vector3 centrePoint;
    GameObject player;
    [SerializeField] float buildingWidth;

    //bool justLeft = false;
    void Start()
    {
        centrePoint = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        OnBuildingExit();
    }

    void Update()
    {
        if (IsInRange())
        {
            OnBuildingEnter();
        }
        else
        {
            OnBuildingExit();
        }
    }

    private bool IsInRange()
    {
        return Vector3.Distance(centrePoint, player.transform.position) < buildingWidth;
    }

    void OnBuildingEnter()
    {
        inner.SetActive(true);
        outer.SetActive(false);
        inside = true;
    }
    void OnBuildingExit()
    {
        inner.SetActive(false);
        outer.SetActive(true);
        inside = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, buildingWidth);
    }
}
