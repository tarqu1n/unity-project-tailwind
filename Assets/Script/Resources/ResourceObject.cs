using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public Config.ResourceTypes type;
    public float startAmount;
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
