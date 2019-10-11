using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public SOBuilding[] buildings;

    private BuildManager buildManager;
    private ResourceManager resourceManager;

    private void Start()
    {
        buildManager = GetComponent<BuildManager>();
        resourceManager = GetComponent<ResourceManager>();
    }

    public void OnBuildBuildingRequest(string name)
    {
        SOBuilding building = GetBuildingByName(name);
        if (building && resourceManager.CanAffordCost(building.cost))
        {
            buildManager.Build(building);
        }
    }

    private SOBuilding GetBuildingByName(string name)
    {
        foreach(SOBuilding building in buildings)
        {
            if (building.name == name)
            {
                return building;
            }
        }

        return null;
    }
}

public enum BuildingType
{
    Tower = 0,
    ResourceGatherer = 1,
}