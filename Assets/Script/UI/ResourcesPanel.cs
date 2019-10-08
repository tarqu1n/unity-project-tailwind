using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesPanel : MonoBehaviour
{
    public TextMeshProUGUI woodAmountText;
    
    private ResourceManager resourceManager;

    private void Start()
    {
        GameObject levelManagerObject = GameObject.Find("/LevelManager");
        resourceManager = levelManagerObject.GetComponent<ResourceManager>();
    }
    private void FixedUpdate()
    {
        woodAmountText.SetText(resourceManager.currentResources[Config.ResourceTypes.Wood].ToString());
    }

}
