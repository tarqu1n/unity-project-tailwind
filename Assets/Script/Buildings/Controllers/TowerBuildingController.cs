using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildingController : BuildingController
{
    private TowerAttackController towerAttackController;
    private TowerTargetingController towerTargetingController;
    private void Start()
    {
        towerAttackController = GetComponent<TowerAttackController>();
        towerTargetingController = GetComponent<TowerTargetingController>();
    }

    public override void Enable()
    {
        towerAttackController.enabled = true;
        towerTargetingController.Enable();
    }
    public override void Disable()
    {
        towerAttackController.enabled = false;
        towerTargetingController.Disable();
    }
}