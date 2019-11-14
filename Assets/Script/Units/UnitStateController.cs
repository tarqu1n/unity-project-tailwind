using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMovementController))]

public class UnitStateController : MonoBehaviour
{
    [Header("Config")]
    public new string name;
    public Config.UnitType type;
    public float startHealth;
    public bool isPlayerControllable;
    public Waypoint spawnPoint;

    [Header("Dependencies")]
    public GameObject model;
    public Behaviour currentBehaviour;

    [Header("Read Only State")]
    public float currentHealth;
    public bool isPlayerSelected = false;   

    public event System.Action<GameObject> OnObjectDestroyed;

    private UnitMovementController unitMovementController;
    private UnitRangeController unitRangeController;

    private void Start()
    {
        unitMovementController = GetComponent<UnitMovementController>();
        unitRangeController = GetComponent<UnitRangeController>();

        currentHealth = startHealth;
    }

    public void RecieveDamage (float damage)
    {
        currentHealth -= damage;
        currentBehaviour.HandleRecieveDamage(damage);
    }

    public void Die()
    {
        currentBehaviour.HandleDie();
        Destroy(gameObject);
    }

    public void SetPlayerSelected (bool selected)
    {
        Debug.Log("Set player selected");
        isPlayerSelected = selected;
        currentBehaviour.HandleSetSelected();
    }

    private void OnDestroy()
    {
        OnObjectDestroyed?.Invoke(gameObject);
        OnObjectDestroyed?.Invoke(model); // the model is where our hitbox is so stuff will care when it gets destroyed
    }

    public void OrderToTarget(RaycastHit hit)
    {
        if (isPlayerControllable) {

            if (hit.transform.gameObject.CompareTag("Unit"))
            {
                GameObject unit = hit.transform.parent.gameObject;
                currentBehaviour.HandleOrderToTargetUnit(unit);
            } else
            {
                unitMovementController.SetTarget(hit.point);
            }
        }
    }
}
