using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetObject : MonoBehaviour
{
    private float permeability = 1.25663706212f * Mathf.Pow(10f, -6f);
    public float MaxForce;

    Vector3 CalculateGilbertForce(Magnet magnet1, Magnet magnet2)
    {
        var m1 = magnet1.transform.position;
        var m2 = magnet2.transform.position;
        var r = m2 - m1;
        var dist = r.magnitude;
        var part0 = permeability * magnet1.MagnetForce * magnet2.MagnetForce;
        var part1 = 4 * Mathf.PI * dist;

        var f = (part0 / part1);

        if (magnet1.MagneticPole == magnet2.MagneticPole)
            f = -f;

        return f * r.normalized;
    }

    private void FixedUpdate()
    {
        Magnet[] magnets = FindObjectsOfType<Magnet>();

        for (int i = 0; i < magnets.Length; i++)
        {
            Vector3 desiredForce = Vector3.zero;

            for (int j = 0; j < magnets.Length; j++)
            {               
                if (i == j)
                    continue;

                desiredForce += CalculateGilbertForce(magnets[i], magnets[j]);
            }

            magnets[i].rigidbody.AddForce(desiredForce, ForceMode.Force);
        }
    }

    
}
