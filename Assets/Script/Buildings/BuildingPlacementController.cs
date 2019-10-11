using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingPlacementController : MonoBehaviour
{
    public bool isPlacing = false;
    public event System.Action<GameObject> onBuildingPlaced;
    public event System.Action<GameObject> onPlacementCancelled;

    private Shader normalShader;
    private Shader whilePlacingShader;
    private Renderer rend;
    private List<int> whilePlacingCollisionIds = new List<int>();
    private BuildingController buildingController;
    private void Start()
    {
        buildingController = GetComponent<BuildingController>();
        normalShader = Shader.Find("Standard");
        whilePlacingShader = Shader.Find("Custom/TransparentTintOutline");
        rend = buildingController.model.GetComponent<Renderer>();

        if (isPlacing)
        {
            SetPlacing(true);
        }
    }
    private void Update()
    {
        if (isPlacing)
        {
            if (Input.GetMouseButtonDown((int)Config.MouseButtonMap.Left) &&
            whilePlacingCollisionIds.Count == 0)
            {
                PlaceBuilding();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CancelPlaceBuilding();
            }
            ShowPlacement();
        }
    }

    private void PlaceBuilding()
    {
        SetPlacing(false);
        this.enabled = false;
        onBuildingPlaced?.Invoke(gameObject);
    }

    public void CancelPlaceBuilding()
    {
        SetPlacing(false);
        onPlacementCancelled?.Invoke(gameObject);
        Destroy(gameObject);
    }

    public void SetPlacing(bool placing)
    {
        if (placing)
        {
            isPlacing = true;
            rend.material.shader = whilePlacingShader;
            buildingController.Disable();
        }
        else
        {
            isPlacing = false;
            rend.material.shader = normalShader;
            buildingController.Enable();
        }
    }

    public void ShowPlacement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, 50f);
        RaycastHit highestHit = new RaycastHit(); // set it so its not undefined
        float highestY = -Mathf.Infinity;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag(Config.tagList["Ground"]) || hit.transform.CompareTag(Config.tagList["Track"]))
            {
                if (hit.point.y > highestY)
                {
                    highestY = hit.point.y;
                    highestHit = hit;
                }

            }
        }

        NavMeshHit navHit;
        if (highestY > -Mathf.Infinity && NavMesh.SamplePosition(highestHit.point, out navHit, 3f, NavMesh.AllAreas))
        {
            transform.position = navHit.position;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        string collisionTag = collision.gameObject.tag;
        if (isPlacing &&
            (collisionTag == Config.tagList["Building"] ||
            collisionTag == Config.tagList["Track"] ||
            collisionTag == Config.tagList["Spawn Point"] ||
            collisionTag == Config.tagList["Terminate Point"] ||
            collisionTag == Config.tagList["Resource Object"] || 
            collisionTag == Config.tagList["Monster"]))
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
