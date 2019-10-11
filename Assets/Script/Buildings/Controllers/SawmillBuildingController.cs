using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawmillBuildingController : BuildingController
{
    public float startHarvestRange = 1f;

    public GameObject rangeObject;

    private GameObject currentTarget;

    private void Start()
    {
        
    }

    private void AquireTarget()
    {

    }

    public override void Enable()
    {
        
    }
    public override void Disable()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.tag);
    }

    public void OnTriggerExit(Collider collision)
    {

    }
}