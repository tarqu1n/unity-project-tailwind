using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public int startHealth;

    public int currentHealth;
    
    public GameObject target;
    private NavMeshAgent navAgent;

    void Start()
    {

        currentHealth = startHealth;

        navAgent = GetComponent<NavMeshAgent>();

        // this may need to be in fixed update to activate avoiding collisions
        if (target)
        {
            navAgent.SetDestination(target.transform.position);
        }
    }

    void Update()
    {
        
    }

    public void RecieveDamage (int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
