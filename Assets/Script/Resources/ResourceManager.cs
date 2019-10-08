using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public int startWood = 0;
    private void Awake()
    {
        SetInitialResources();
    }

    public void IncrementResource(Config.ResourceTypes type, int value)
    {
        currentResources[type] += value;
        currentResources[type] = Mathf.Max(currentResources[type], 0);
    }

    public bool CanAffordCost(ResourceCost[] cost)
    {
        foreach (ResourceCost c in cost)
        {
            if (!CanAffordCost(c))
            {
                return false;
            }
        }

        return true;
    }

    public bool CanAffordCost(ResourceCost cost)
    {
        return currentResources[cost.type] >= cost.amount;
    }
    
    public void SpendCost(ResourceCost[] cost) {
        foreach (ResourceCost c in cost)
        {
            SpendCost(c);
        }
    }
    public void SpendCost(ResourceCost cost)
    {
        currentResources[cost.type] -= cost.amount;
    }

    private void SetInitialResources()
    {
        currentResources[Config.ResourceTypes.Wood] = startWood;
    }
        
    public IDictionary<Config.ResourceTypes, int> currentResources = new Dictionary<Config.ResourceTypes, int>()
    {
        { Config.ResourceTypes.Wood, 0 }
    };
}

[System.Serializable]
public class ResourceCost
{
    public Config.ResourceTypes type;
    public int amount;
}

