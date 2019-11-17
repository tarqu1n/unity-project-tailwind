using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovementController : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public float targetStopDistance = 0.1f;

    [Header("Read Only")]
    public UnitMovementTarget currentTarget;

    private UnitStateController unitStateController;

    private void Start()
    {
        unitStateController = GetComponent<UnitStateController>();
    }

    public void SetCanMove (bool canMove)
    {
        navAgent.isStopped = !canMove;
    }

    public void SetTarget (Vector3 pos)
    {
        currentTarget = new UnitMovementTarget()
        {
            type = UnitMovementTarget.Type.Position,
            position = pos
        };

        navAgent.autoBraking = true;
        navAgent.SetDestination(currentTarget.position);
        unitStateController.currentBehaviour.HandleSetMoveTarget();
    }

    public void SetTarget (Waypoint waypoint)
    {
        currentTarget = new UnitMovementTarget()
        {
            type = UnitMovementTarget.Type.Waypoint,
            waypoint = waypoint,
            position = waypoint.transform.position
        };

        navAgent.autoBraking = false;
        navAgent.SetDestination(currentTarget.position);
        unitStateController.currentBehaviour.HandleSetMoveTarget();
    }

    public void SetTarget (GameObject obj, UnitMovementTarget.Type type)
    {
        currentTarget = new UnitMovementTarget()
        {
            type = type,
            gameObject = obj,
        };

        if (type == UnitMovementTarget.Type.Unit)
        {
            obj.GetComponent<UnitStateController>().OnObjectDestroyed += OnMovementTargetDestroyed;
        }
        navAgent.autoBraking = true;
        navAgent.SetDestination(currentTarget.gameObject.transform.position);
        unitStateController.currentBehaviour.HandleSetMoveTarget();
    }

    public void ClearCurrentTarget()
    {
        this.currentTarget = null;
        navAgent.ResetPath();
    }

    private void FixedUpdate()
    {
        if (currentTarget != null && !navAgent.pathPending && navAgent.remainingDistance < 0.1f)
        {
            unitStateController.currentBehaviour.HandleMoveTargetInRange();
        }

        if (currentTarget != null && currentTarget.type == UnitMovementTarget.Type.Unit)
        {
            navAgent.SetDestination(currentTarget.gameObject.transform.position);
        }

    }

    public void OnMovementTargetDestroyed(GameObject obj)
    {
        ClearCurrentTarget();
    }
}

public class UnitMovementTarget
{
    public Type type;
    public Vector3 position;
    public GameObject gameObject;
    public Waypoint waypoint;

    public enum Type
    {
        Position,
        Waypoint,
        Unit,
    }
}
