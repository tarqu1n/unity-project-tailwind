using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public new string name;
    public float startHealth;

    public GameObject mesh;

    [Header("Read Only")]
    public float currentHealth;
    public GameObject target;

    public event System.Action<GameObject> OnObjectDestroyed;

    private MonsterManager monsterManager;
    private NavMeshAgent navAgent;
    void Start()
    {
        GameObject levelManagerObject = GameObject.Find("/LevelManager");
        monsterManager = levelManagerObject.GetComponent<MonsterManager>();

        currentHealth = startHealth;
        navAgent = mesh.GetComponent<NavMeshAgent>();

        // this may need to be in fixed update to activate avoiding collisions
        if (target)
        {
            navAgent.SetDestination(target.transform.position);
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        navAgent.SetDestination(target.transform.position);
    }

    public void RecieveDamage (float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        monsterManager.OnMonsterDie(gameObject);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // TODO: Fiddle with this to work out how C# events work and feed back when destroyed
        OnObjectDestroyed?.Invoke(gameObject);
    }

    void Escape()
    {
        monsterManager.OnMonsterEscape(gameObject);
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == Config.tagList["Terminate Point"])
        {
            Escape();
        }
    }
}
