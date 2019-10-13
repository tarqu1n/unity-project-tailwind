using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputManager : MonoBehaviour
{
    private UserSelectionManager userSelectionManager;
    private void Start()
    {
        userSelectionManager = GetComponent<UserSelectionManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown((int)Config.MouseButtonMap.Left))
        {
            HandleLeftMouseClick();
        }
        if (Input.GetMouseButtonDown((int)Config.MouseButtonMap.Right))
        {
            HandleRightMouseClick();
        }
    }

    void HandleRightMouseClick()
    {
        IssueMapOrderToSelected();
    }

    void HandleLeftMouseClick()
    {
        userSelectionManager.ClearAllSelected();

        HandleClickSingleTarget();
    }

    void IssueMapOrderToSelected()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            userSelectionManager.IssueMapOrder(hit);
        }
        
    }

    void HandleClickSingleTarget() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        userSelectionManager.ClearAllSelected();
        if (Physics.Raycast(ray, out hit))
        {
            userSelectionManager.SetObjectSelected(hit.transform.gameObject);
        }
    }

}
