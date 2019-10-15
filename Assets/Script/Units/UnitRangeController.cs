using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRangeController : MonoBehaviour
{
    UnitStateController unitStateController;
    List<GameObject> inRangeUnits;

    void Start()
    {
        unitStateController = transform.parent.GetComponent<UnitStateController>();
        inRangeUnits = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(Config.tagList["Unit"]))
        {
            GameObject unit = collider.transform.parent.gameObject; // our hitbox is always a child of the main unit
            if (unit != transform.parent.gameObject) // ensure we're not recording ourselves
            {
                inRangeUnits.Add(unit);
                unit.GetComponent<UnitStateController>().OnObjectDestroyed += HandleInRangeUnitDie;
                unitStateController.currentBehaviour.HandleUnitEnterRange(unit);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(Config.tagList["Unit"])) { 
            GameObject unit = collider.transform.parent.gameObject; // our hitbox is always a child of the main unit
            if (inRangeUnits.Contains(unit))
            {
                unit.GetComponent<UnitStateController>().OnObjectDestroyed -= HandleInRangeUnitDie;
                unitStateController.currentBehaviour.HandleUnitExitRange(unit);
                inRangeUnits.Remove(unit);
            }
        }
    }

    private void HandleInRangeUnitDie(GameObject gameObject)
    {
        for (int i = inRangeUnits.Count - 1; i >= 0; i--)
        {
            if (inRangeUnits[i] == gameObject)
            {
                unitStateController.currentBehaviour.HandleUnitExitRange(inRangeUnits[i]);
                inRangeUnits.Remove(inRangeUnits[i]);
                break;
            }
        }
    }
}
