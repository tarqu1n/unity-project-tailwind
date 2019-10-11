using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BuildingController : MonoBehaviour
{
    public BuildingType buildingType;
    public ResourceCost[] cost;

    [Header("Read Only")]
    public GameObject model;

    public abstract void Disable();
    public abstract void Enable();
}




