using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBehaviour : Behaviour
{
    private void Start()
    {
        unitActionController = GetComponent<UnitActionController>();
        unitMovementController = GetComponent<UnitMovementController>();
    }

    public override void HandleSetMoveTarget() { }
    public override void HandleSetSelected() { }

    public override void HandleMoveTargetInRange() { }

    public override void HandleRecieveDamage(float damage) { }

    public new void HandleDie() { }

    public override void HandleCollisionWithTerminatePoint(Collider collision) { }

    public override void HandleUnitEnterRange(GameObject gameObject) { }

    public override void HandleUnitExitRange(GameObject gameObject) { }

    public override void HandleDidAttackCurrentTarget() {}

    public override void HandleAttackTargetSet() {
        unitMovementController.SetTarget(unitActionController.attackTarget, UnitMovementTarget.Type.Unit);
    }

    public override void HandleOrderToTargetUnit(GameObject unit) {
        Debug.Log("handle order to target unit");
        if (unit.GetComponent<UnitStateController>().type == Config.UnitType.Monster)
        {
            unitActionController.SetAttackTarget(unit);
        }
    }
}
