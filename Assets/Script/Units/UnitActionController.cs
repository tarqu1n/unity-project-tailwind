using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionController : MonoBehaviour
{
    GameObject weapon;
    public GameObject attackTarget;

    private int weaponRange = 1;
    private int weaponDamage = 1;
    private int weaponAttackSpeed = 1;
    private float WeaponAttackTimer = 0;

    private UnitStateController unitStateController;
    private UnitMovementController unitMovementController;
    void Start()
    {
        unitStateController = GetComponent<UnitStateController>();
        unitMovementController = GetComponent<UnitMovementController>();
    }

    void Update()
    {
        if (WeaponAttackTimer > 0)
        {
            WeaponAttackTimer -= Time.deltaTime;
        } else if (attackTarget)
        {
            AttackTarget();
        }
    }

    public void SetAttackTarget(GameObject target)
    {
        attackTarget = target;
        UnitStateController targetStateController = target.GetComponent<UnitStateController>();

        targetStateController.OnObjectDestroyed += OnAttackTargetUnitDie;
        unitStateController.currentBehaviour.HandleAttackTargetSet();
    }

    public void OnAttackTargetUnitDie(GameObject gameObject)
    {
        attackTarget = null;
    }

    public void AttackTarget()
    {
        if (attackTarget)
        {
            UnitStateController targetStateController = attackTarget.GetComponent<UnitStateController>();
            if (Vector3.Distance(attackTarget.transform.position, transform.position) <= weaponRange)
            {
                targetStateController.RecieveDamage(weaponDamage);
                WeaponAttackTimer = weaponAttackSpeed;
                unitStateController.currentBehaviour.HandleDidAttackCurrentTarget();
                Debug.Log("Attack" + unitStateController.name);
            }
        }
    }
}
