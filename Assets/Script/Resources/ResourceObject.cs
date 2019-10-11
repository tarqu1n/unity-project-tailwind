using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public Config.ResourceObjectType objectType;
    public Config.ResourceTypes resourceType;
    public float startAmount;

    [Header("Read Only")]
    public float currentAmount;

    void Start()
    {
        currentAmount = startAmount;    
    }

    void RemoveResource (float amount)
    {
        currentAmount -= amount;
        if (currentAmount < 0)
        {
            OnEmpty();
        }
    }

    void OnEmpty()
    {
        Debug.Log("is empty");
    }
}
