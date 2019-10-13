using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHitboxController : MonoBehaviour
{
    private UnitStateController unitStateController;
    private void Awake()
    {
        unitStateController = gameObject.transform.parent.gameObject.GetComponent<UnitStateController>();
    }
    public void OnTriggerEnter(Collider collision)
    {
        Behaviour currentBehaviour = unitStateController.currentBehaviour;

        if (currentBehaviour)
        {
            if (collision.gameObject.tag == Config.tagList["Terminate Point"])
            {
                currentBehaviour.HandleCollisionWithTerminatePoint(collision);
            }
        }
    }
}
