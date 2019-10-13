using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSelectionManager : MonoBehaviour
{
    public List<SelectedObject> selected = new List<SelectedObject>();

    public void SetObjectSelected (GameObject gameObject)
    {
        if (gameObject.tag == Config.tagList["Unit"])
        {
            SetUnitSelected(gameObject.transform.parent.gameObject);
        }
    }

    public void SetUnitSelected (GameObject gameObject)
    {
        SelectedObject newSelectedObject = new SelectedObject()
        {
            type = SelectedObject.Type.Unit,
            gameObject = gameObject,
        };

        selected.Add(newSelectedObject);

        UnitStateController unitController = gameObject.GetComponent<UnitStateController>();
        if (unitController)
            unitController.SetPlayerSelected(true);
    }

    public void ClearAllSelected()
    {
        for (int i = selected.Count - 1; i >= 0; i--)
        {
            SelectedObject s = selected[i];

            selected.Remove(s);

            if (s.type == SelectedObject.Type.Unit)
            {
                UnitStateController unitController = s.gameObject.GetComponent<UnitStateController>();
                if (unitController)
                    unitController.SetPlayerSelected(false);
            }
        }
    }

    public void IssueMapOrder(RaycastHit hit)
    {
        foreach (SelectedObject s in selected)
        {
            if (s.type == SelectedObject.Type.Unit)
            {
                UnitStateController unitController = s.gameObject.GetComponent<UnitStateController>();
                if (unitController)
                    unitController.OrderToTarget(hit);
            }
        }
    }

    public class SelectedObject
    {
        public Type type;
        public GameObject gameObject;
        
        public enum Type
        {
            Unit,
            Building
        }
    }
}
