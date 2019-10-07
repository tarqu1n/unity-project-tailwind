using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public BuildingType buildingType;

    [Header("Read Only")]
    public GameObject model;

    private bool isPlacing = false;
    public void SetPlacing(bool placing)
    {
        if (!isPlacing)
        {
            isPlacing = true;
            Disable();
        } else
        {
            isPlacing = true;
            Enable();
        }
    }

    public void Disable()
    {
        if (buildingType == BuildingType.Tower)
        {
            gameObject.GetComponent<TowerAttackController>().active = false;
        }
    }

    public void Enable()
    {
        if (buildingType == BuildingType.Tower)
        {
            gameObject.GetComponent<TowerAttackController>().active = true;
        }
    }
}


