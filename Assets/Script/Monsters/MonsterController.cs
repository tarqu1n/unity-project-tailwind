using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MonsterMovementController))]
public class MonsterController : MonoBehaviour
{
    public new string name;
    public float startHealth;

    public GameObject mesh;

    [Header("Read Only")]
    public float currentHealth;
    public NavMeshAgent navAgent;

    public event System.Action<GameObject> OnObjectDestroyed;

    private MonsterManager monsterManager;
    void Start()
    {
        GameObject levelManagerObject = GameObject.Find("/LevelManager");
        monsterManager = levelManagerObject.GetComponent<MonsterManager>();

        currentHealth = startHealth;
        navAgent = mesh.GetComponent<NavMeshAgent>();
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
        OnObjectDestroyed?.Invoke(gameObject);
        // the mesh is where our hitbox is so stuff will care when gets destroyed too
        OnObjectDestroyed?.Invoke(mesh);
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
