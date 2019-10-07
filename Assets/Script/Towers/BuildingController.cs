using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public BuildingType buildingType;

    [Header("Read Only")]
    public GameObject model;
    public bool isPlacing = false;

    private Shader normalShader;
    private Shader whilePlacingShader;
    private Renderer rend;
    private List<int> whilePlacingCollisionIds = new List<int>();

    private void Start()
    {
        normalShader = Shader.Find("Standard");
        whilePlacingShader = Shader.Find("Custom/TransparentTintOutline");
        rend = model.GetComponent<Renderer>();

        if (isPlacing)
        {
            SetPlacing(true);
        }
    }

    public void SetPlacing(bool placing)
    {
        if (isPlacing)
        {
            isPlacing = true;
            rend.material.shader = whilePlacingShader;
            Disable();
        } else
        {
            isPlacing = false;
            rend.material.shader = normalShader;
            Enable();
        }
    }

    public void Disable()
    {
        if (buildingType == BuildingType.Tower)
        {
            gameObject.GetComponent<TowerAttackController>().active = false;
            gameObject.GetComponent<TowerTargetingController>().Disable();
        }
    }

    public void Enable()
    {
        if (buildingType == BuildingType.Tower)
        {
            gameObject.GetComponent<TowerAttackController>().active = true;
            gameObject.GetComponent<TowerTargetingController>().Enable();
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (isPlacing && 
            collisionTag == Config.tagList["Building"] || 
            collisionTag == Config.tagList["Spawn Point"] || 
            collisionTag == Config.tagList["Terminate Point"] ||
            collisionTag == Config.tagList["Monster"])
        {
            whilePlacingCollisionIds.Add(collision.gameObject.GetInstanceID());
            rend.material.SetColor("_TintColour", Color.red);
            
            if (collisionTag == Config.tagList["Monster"])
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<MonsterController>().OnObjectDestroyed += OnCollidedMonsterDestroyed;
            }
        }

    }

    private void OnCollidedMonsterDestroyed(GameObject gameObject)
    {
        Debug.Log("oncollidedmonsterdestroyed");
        // TODO: doesnt work because its a different game object
        if (whilePlacingCollisionIds.Contains(gameObject.GetInstanceID()))
        {
            whilePlacingCollisionIds.Remove(gameObject.GetInstanceID());
            if (whilePlacingCollisionIds.Count == 0)
            {
                rend.material.SetColor("_TintColour", Color.white);
            }
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        int colliderId = collision.gameObject.GetInstanceID();
        if (whilePlacingCollisionIds.Contains(colliderId))
        {
            whilePlacingCollisionIds.Remove(colliderId);

            if (whilePlacingCollisionIds.Count == 0)
            {
                rend.material.SetColor("_TintColour", Color.white);
            }
        }
    }
}


