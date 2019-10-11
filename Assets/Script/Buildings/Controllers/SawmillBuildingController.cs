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
        rangeObject.GetComponent<SphereCollider>().enabled = true;
    }
    public override void Disable()
    {
        rangeObject.GetComponent<SphereCollider>().enabled = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(Config.tagList["Resource Object"]))
        {
            Debug.Log(collision.gameObject.tag);
        }
    }

    public void OnTriggerExit(Collider collision)
    {

    }
}