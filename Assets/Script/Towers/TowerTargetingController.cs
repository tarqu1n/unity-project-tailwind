using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargetingController : MonoBehaviour
{

    public float startRange;
    public GameObject rangeObject;

    [Header("Readonly")]
    public GameObject currentTarget;
    private float currentRange;

    private List<Collider> inRangeColliderList = new List<Collider>();
    private SphereCollider sphereCollider;
    void Awake()
    {
        sphereCollider = rangeObject.GetComponent<SphereCollider>();

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
        if (!currentTarget && inRangeColliderList.Count > 0)
        {
            Collider selectedCollider = null;
            float selectedColliderDistance = Mathf.Infinity;
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

            // if monster was destroyed during loop
            if (!selectedCollider)
            {
                inRangeColliderList.Remove(selectedCollider);
                AquireTarget();
                return;
            }
            currentTarget = selectedCollider.gameObject;
        } else if (!currentTarget)
        {
            currentTarget = null;
        }
    }

    public void Disable()
    {
        sphereCollider.enabled = false;
    }

    public void Enable()
    {
        sphereCollider.enabled = true;
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

            if (!currentTarget || currentTarget.GetInstanceID() == target.gameObject.GetInstanceID())
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
