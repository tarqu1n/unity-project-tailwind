using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildManager : MonoBehaviour
{
    private bool isPlacing = false;

    private SOBuilding currentBuilding;
    private GameObject currentBuildingInstance;
    private BuildingController currentBuildingInstanceController;
    public void Build(SOBuilding building)
    {
        isPlacing = true;
        currentBuilding = building;

        currentBuildingInstance = Instantiate(building.prefab);
        currentBuildingInstanceController = currentBuildingInstance.GetComponent<BuildingController>();
        currentBuildingInstanceController.SetPlacing(true);
    }

    public void FixedUpdate()
    {
        if (isPlacing && currentBuilding)
        {
            ShowPlacement();
        }
    }

    public void ShowPlacement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, 50f);
        RaycastHit highestHit = new RaycastHit(); // set it so its not undefined
        float highestY = -Mathf.Infinity;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag(Config.tagList["Ground"]))
            {
                if (hit.point.y > highestY)
                {
                    highestY = hit.point.y;
                    highestHit = hit;
                }
                
            }
        }

        NavMeshHit navHit;
        if (highestY > -Mathf.Infinity && NavMesh.SamplePosition(highestHit.point, out navHit, 3f, NavMesh.AllAreas))
        {
            currentBuildingInstance.transform.position = navHit.position;
        }
    }
}
