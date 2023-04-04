using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class BuildingEnterScript : MonoBehaviour
{
    [SerializeField] GameObject inner, outer, mainLighting;
    Vector3 centrePoint;
    GameObject player;
    [SerializeField] float buildingWidth;
    void Start()
    {
        centrePoint = this.transform.position;
        OnBuildingExit();
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (IsInRange())
        {
            print("Entered");
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

    public void OnBuildingEnter()
    {
        inner.SetActive(true);
        outer.SetActive(false);
        mainLighting.SetActive(false);
        print("Entered");
    }
    public void OnBuildingExit()
    {

        inner.SetActive(false);
        outer.SetActive(true);
        mainLighting.SetActive(true);
        print("Exited");
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, buildingWidth);
    }
}
