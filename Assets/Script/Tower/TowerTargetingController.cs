using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargetingController : MonoBehaviour
{
    public GameObject currentTarget;
    public float startRange;

    private float currentRange;

    private List<Collider> inRangeColliderList = new List<Collider>();
    private SphereCollider sphereCollider;
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();

        SetRange(startRange);
    }


    void SetRange (float newRange)
    {
        currentRange = newRange;
        sphereCollider.radius = currentRange;
    }

    void FixedUpdate()
    {
        if (!currentTarget)
        {
            AquireTarget();
        }
    }

    void AquireTarget()
    {
        Collider selectedCollider = null;
        float selectedColliderDistance = Mathf.Infinity;

        if (inRangeColliderList.Count > 0)
        {
            
            //TODO: this could be a point for optimisation (dont clone collider list but store and remove any colliders that no longer exist)
            foreach (Collider collider in inRangeColliderList.ToArray())
            {
                // if monster has been destroyed then its collider wont exist so remove it
                if (!collider)
                {
                    inRangeColliderList.Remove(collider);
                    continue;
                }
                float distance = Vector3.Distance(transform.position, collider.gameObject.transform.position);

                if (distance < selectedColliderDistance)
                {
                    selectedCollider = collider;
                    selectedColliderDistance = distance;
                }
            }

            currentTarget = selectedCollider.gameObject;
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == Config.tagList["Monster"] && !inRangeColliderList.Contains(target))
        {
            inRangeColliderList.Add(target);
        }
    }

    private void OnTriggerExit(Collider target)
    {
        if (target.tag == Config.tagList["Monster"] && inRangeColliderList.Contains(target))
        {
            inRangeColliderList.Remove(target);
            
            if (currentTarget.GetInstanceID() == target.gameObject.GetInstanceID())
            {
                currentTarget = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, startRange);
    }
}
