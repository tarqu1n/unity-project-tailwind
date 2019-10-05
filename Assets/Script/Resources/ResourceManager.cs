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

    private void SetInitialResources()
    {
        currentResources[Config.ResourceTypes.Wood] = startWood;
    }


    public IDictionary<Config.ResourceTypes, int> currentResources = new Dictionary<Config.ResourceTypes, int>()
    {
        { Config.ResourceTypes.Wood, 0 }
    };

}

