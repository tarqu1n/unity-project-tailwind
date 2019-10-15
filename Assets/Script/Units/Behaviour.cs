using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour : MonoBehaviour
{
    [Header("Read Only")]
    public UnitStateController unitStateController;
    public UnitMovementController unitMovementController;
    private void Start()
    {
        unitStateController = GetComponent<UnitStateController>();
        unitMovementController = GetComponent<UnitMovementController>();
    }

    public abstract void HandleSetSelected();
    public abstract void HandleRecieveDamage(float damage);
    public abstract void HandleSetMoveTarget();
    public abstract void HandleMoveTargetInRange();
    public void HandleDie() { }
    public abstract void HandleCollisionWithTerminatePoint(Collider collision);
    public abstract void HandleUnitEnterRange(GameObject gameObject);
    public abstract void HandleUnitExitRange(GameObject gameObject);
}
