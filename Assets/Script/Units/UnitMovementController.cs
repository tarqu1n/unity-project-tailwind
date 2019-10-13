using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementController : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public float targetStopDistance = 0.1f;

    [Header("Read Only")]
    public UnitTarget currentTarget;

    private UnitStateController unitStateController;

    private void Start()
    {
        unitStateController = GetComponent<UnitStateController>();
    }

    public void SetCanMove (bool canMove)
    {
        navAgent.isStopped = canMove;
    }

    public void SetTarget (Vector3 pos)
    {
        currentTarget = new UnitTarget()
        {
            type = UnitTarget.Type.Position,
            position = pos
        };

        navAgent.autoBraking = true;
        navAgent.SetDestination(currentTarget.position);
        unitStateController.currentBehaviour.HandleSetMoveTarget();
    }

    public void SetTarget (Waypoint waypoint)
    {
        currentTarget = new UnitTarget()
        {
            type = UnitTarget.Type.Waypoint,
            waypoint = waypoint,
            position = waypoint.transform.position
        };

        navAgent.autoBraking = false;
        navAgent.SetDestination(currentTarget.position);
        unitStateController.currentBehaviour.HandleSetMoveTarget();
    }

    public void SetTarget (GameObject obj, bool isStatic)
    {
        currentTarget = new UnitTarget()
        {
            type = isStatic ? UnitTarget.Type.StaticObject : UnitTarget.Type.DynamicObject,
            gameObject = obj,
            position = obj.transform.position
        };

        navAgent.autoBraking = true;
        navAgent.SetDestination(currentTarget.position);
        unitStateController.currentBehaviour.HandleSetMoveTarget();
    }

    public void ClearCurrentTarget()
    {
        this.currentTarget = null;
    }

    private void FixedUpdate()
    {
        if (currentTarget != null && !navAgent.pathPending && navAgent.remainingDistance < 0.1f)
        {
            unitStateController.currentBehaviour.HandleMoveTargetInRange();
        }
    }
}

public class UnitTarget
{
    public Type type;
    public Vector3 position;
    public GameObject gameObject;
    public Waypoint waypoint;

    public enum Type
    {
        Position,
        Waypoint,
        StaticObject,
        DynamicObject,
    }
}
