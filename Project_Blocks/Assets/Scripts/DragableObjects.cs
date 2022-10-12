using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObjects : MonoBehaviour
{
    private GameObject selectedObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectObject();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            DeselectObject();
        }
    }

    private void SelectObject()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000))
        {
            if (hitInfo.transform.gameObject.CompareTag("Moveable"))
            {
                selectedObject = hitInfo.transform.gameObject;
                print(selectedObject);
            }
        }
    }

    private void DeselectObject()
    {
        selectedObject = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition));
    }
}
