using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TowerTargetingController))]

public class TowerAttackController : MonoBehaviour
{
    public bool active = true;
    public float startAttackSpeed = 1f;
    public GameObject projectileObject;
    public GameObject projectileSpawn;

    private float currentAttackSpeed;
    private TowerTargetingController targetingController;
    private float currentAttackTimer;

    void Start()
    {
        currentAttackSpeed = startAttackSpeed;
        targetingController = GetComponent<TowerTargetingController>();

        currentAttackTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (currentAttackTimer > 0f)
        {
            currentAttackTimer -= Time.deltaTime;
        }

        if (currentAttackTimer <= 0f)
        {
            Attack();
        }
    }

    public void Attack()
    {
        
        if (targetingController.currentTarget)
        {
            GameObject instance = Instantiate(projectileObject, projectileSpawn.transform.position, Quaternion.identity);
            instance.GetComponent<Projectile>().target = targetingController.currentTarget;

            currentAttackTimer = currentAttackSpeed;
        }
    }
}
