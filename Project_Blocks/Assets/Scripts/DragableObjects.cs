using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObjects : MonoBehaviour
{
    public Vector3 mousePosOffset;
    public float dragSpeed;

    private GameObject selectedObject;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            DeselectObject();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectObject();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            MoveSelectedObject();
        }
    }

    private void MoveSelectedObject()
    {
        if (selectedObject == null)
            return;

        Rigidbody rb = selectedObject.GetComponent<Rigidbody>();

        Vector3 forceDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition + mousePosOffset) - selectedObject.transform.position;
        Vector3 targetSpeed = forceDirection / (Time.fixedDeltaTime * dragSpeed);

        rb.AddForce(targetSpeed - rb.velocity, ForceMode.Impulse);
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
        if (selectedObject == null)
            return;
        Gizmos.DrawLine(selectedObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition + mousePosOffset));
    }
}
