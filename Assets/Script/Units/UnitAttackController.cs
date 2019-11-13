using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackController : MonoBehaviour
{
    UnitStateController unitStateController;
    GameObject weapon;

    void Start()
    {
        unitStateController = transform.parent.GetComponent<UnitStateController>();
    }

    void Update()
    {
        
    }

    public void AttackTarget()
    {
        if (unitStateController.currentTargetUnit)
        {
            // if unit is in range then attack the unit
        }
    }
}
