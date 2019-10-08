using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildManager : MonoBehaviour
{
    private bool isPlacing = false;

    private SOBuilding currentBuilding;
    private GameObject currentBuildingInstance;
    private BuildingPlacementController currentBuildingPlacementController;
    private ResourceManager resourceManager;
    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
    }
    private void FixedUpdate()
    {
        if (isPlacing)
        {
            if (!resourceManager.CanAffordCost(currentBuilding.cost))
            {
                currentBuildingPlacementController.CancelPlaceBuilding();
            }
        }
    }
    public void Build(SOBuilding building)
    {
        isPlacing = true;
        currentBuilding = building;

        currentBuildingInstance = Instantiate(building.prefab);
        currentBuildingPlacementController = currentBuildingInstance.GetComponent<BuildingPlacementController>();
        currentBuildingPlacementController.enabled = true;
        currentBuildingPlacementController.isPlacing = true;
        currentBuildingPlacementController.onBuildingPlaced += HandleBuildingPlaced;
        currentBuildingPlacementController.onPlacementCancelled += HandlePlacementCancelled;
    }

    public void HandleBuildingPlaced(GameObject building)
    {
        resourceManager.SpendCost(currentBuilding.cost);

        isPlacing = false;
        currentBuildingPlacementController.onBuildingPlaced -= HandleBuildingPlaced;
        currentBuildingPlacementController.onPlacementCancelled -= HandlePlacementCancelled;
        currentBuildingInstance = null;
        currentBuilding = null;
    }

    public void HandlePlacementCancelled(GameObject building)
    {
        isPlacing = false;
        currentBuildingInstance = null;
        currentBuilding = null;
    }
}
