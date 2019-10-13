using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MonsterController))]
public class MonsterMovementController : MonoBehaviour
{
    public Waypoint currentWaypoint;
    
    private CurrentMMTarget currentTarget;

    private MonsterController monsterController;
    private NavMeshAgent navAgent;

    void Start()
    {
        monsterController = GetComponent<MonsterController>();
        navAgent = monsterController.navAgent;

        if (currentTarget == null && currentWaypoint)
        {
            SetCurrentTargetFromWaypoint();
        }
    }

    private void SetCurrentTargetFromWaypoint()
    {
        if (currentWaypoint)
        {
            Waypoint nextWaypoint = currentWaypoint.GetNext();

            if (!nextWaypoint)
            {
                currentWaypoint = null;
                return;
            }

            currentTarget = new CurrentMMTarget()
            {
                type = CurrentMMTarget.Type.Waypoint,
                obj = nextWaypoint.gameObject,
            };
            currentWaypoint = nextWaypoint;
            navAgent.autoBraking = false;
            navAgent.SetDestination(currentTarget.obj.transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (currentTarget != null)
        {
            if (currentTarget.type == CurrentMMTarget.Type.Waypoint && !navAgent.pathPending && navAgent.remainingDistance < 0.1f)
            {
                SetCurrentTargetFromWaypoint();
            }
        }
    }

    private class CurrentMMTarget
    {

        public Type type;
        public GameObject obj;

        public enum Type
        {
            Waypoint
        }
    }

    
}
