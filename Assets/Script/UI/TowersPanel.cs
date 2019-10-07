using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersPanel : MonoBehaviour
{
    public GameObject towerButton;

    private BuildingManager buildingManager;
    void Start()
    {
        GameObject levelManagerObject = GameObject.Find("/LevelManager");
        buildingManager = levelManagerObject.GetComponent<BuildingManager>();

        BuildButtons();
    }

    void BuildButtons()
    {
        foreach (SOBuilding building in buildingManager.towers)
        {
            GameObject instance = Instantiate(towerButton, transform);
            instance.GetComponent<TowerButton>().buildingData = building;
        }
    }
}
