using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public SOBuilding[] towers;

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = GetComponent<BuildManager>();
    }
    public void OnTowerBuildRequest(string name)
    {
        SOBuilding tower = GetTowerByName(name);
        if (tower)
        {
            buildManager.Build(tower);
        }
    }

    private SOBuilding GetTowerByName(string name)
    {
        foreach(SOBuilding tower in towers)
        {
            if (tower.name == name)
            {
                return tower;
            }
        }

        return null;
    }
}

public enum BuildingType
{
    Tower = 0,
}