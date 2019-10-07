using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public SOBuilding buildingData;

    private BuildingManager buildingManager;
    void Start()
    {
        GameObject levelManagerObject = GameObject.Find("/LevelManager");
        buildingManager = levelManagerObject.GetComponent<BuildingManager>();

        GetComponent<Image>().sprite = buildingData.thumbnail;
        GetComponent<Button>().onClick.AddListener(OnBtnClick);
    }
    private void OnBtnClick()
    {
        buildingManager.OnTowerBuildRequest(buildingData.name);
    }


}
