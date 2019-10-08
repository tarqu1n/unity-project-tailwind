using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public BuildingType buildingType;

    [Header("Read Only")]
    public GameObject model;
    

    private void Start()
    {
        
    }

    public void Disable()
    {
        if (buildingType == BuildingType.Tower)
        {
            gameObject.GetComponent<TowerAttackController>().enabled = false;
            gameObject.GetComponent<TowerTargetingController>().Disable();
        }
    }

    public void Enable()
    {
        if (buildingType == BuildingType.Tower)
        {
            gameObject.GetComponent<TowerAttackController>().enabled = true;
            gameObject.GetComponent<TowerTargetingController>().Enable();
        }
    }
}


