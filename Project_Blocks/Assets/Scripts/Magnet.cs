using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float MagnetForce;
    public bool MagneticPole;
    public new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = gameObject.GetComponentInParent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        if (MagneticPole)
            Gizmos.color = Color.blue;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.2f * MagnetForce);
    }
}
