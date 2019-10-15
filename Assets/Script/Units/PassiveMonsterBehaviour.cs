using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMonsterBehaviour : Behaviour
{
    private MonsterManager monsterManager;
    private void Start()
    {
        GameObject levelManagerObject = GameObject.Find("/LevelManager");
        monsterManager = levelManagerObject.GetComponent<MonsterManager>();

        unitStateController = GetComponent<UnitStateController>();
        unitMovementController = GetComponent<UnitMovementController>();

        if (unitStateController.spawnPoint)
        {
            unitMovementController.SetTarget(unitStateController.spawnPoint);
        }
    }
    public override void HandleSetMoveTarget() { }
    public override void HandleSetSelected() { }

    public override void HandleMoveTargetInRange() {
        UnitTarget currentTarget = unitMovementController.currentTarget;

        if (currentTarget.type == UnitTarget.Type.Waypoint)
        {
            Waypoint nextWaypoint = currentTarget.waypoint.GetNext();
            if (nextWaypoint)
            {
                unitMovementController.SetTarget(nextWaypoint);
            }
            else
            {
                unitMovementController.ClearCurrentTarget();
            }
        }
    }

    public override void HandleRecieveDamage(float damage)
    {
        if (unitStateController.currentHealth <= 0)
        {
            unitStateController.Die();
        }
    }

    public new void HandleDie() { }

    public override void HandleCollisionWithTerminatePoint(Collider collision)
    {
        monsterManager.OnMonsterEscape(gameObject);
        Destroy(gameObject);
    }

    public override void HandleUnitEnterRange(GameObject gameObject)
    {
        if (gameObject.GetComponent<UnitStateController>().type == Config.UnitType.Hero) 
        {
            unitStateController.model.GetComponent<Renderer>().material.color = Color.red;
        }
        
    }

    public override void HandleUnitExitRange(GameObject gameObject)
    {
        if (gameObject.GetComponent<UnitStateController>().type == Config.UnitType.Hero)
        {
            unitStateController.model.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
