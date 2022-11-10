using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagneticPole
{
    south,
    north,
    metal
}

public class Magnet : MonoBehaviour
{
    public float MagnetForce;
    public MagneticPole magneticPole;
    public new Rigidbody rigidbody;

    private void OnEnable()
    {
        MagnetObject.Pool.Add(this);
    }

    private void OnDisable()
    {
        MagnetObject.Pool.Remove(this);
    }

    private void Awake()
    {
        rigidbody = gameObject.GetComponentInParent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        if (magneticPole == MagneticPole.south)
            Gizmos.color = Color.green;
        else if (magneticPole == MagneticPole.north)
            Gizmos.color = Color.red;
        else if (magneticPole == MagneticPole.metal)
            Gizmos.color = Color.grey;

        Gizmos.DrawSphere(transform.position, 0.2f * MagnetForce);
    }
}
