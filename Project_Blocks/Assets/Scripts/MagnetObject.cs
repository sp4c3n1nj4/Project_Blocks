using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetObject : MonoBehaviour
{
    public float MaxForce;

    Vector3 CalculateGilbertForce(Magnet magnet1, Magnet magnet2)
    {
        Vector3 r = magnet2.transform.position - magnet1.transform.position;
        float distance = r.magnitude;

        float force = (magnet1.MagnetForce * magnet2.MagnetForce) / Mathf.Pow(distance, 2f);

        if (magnet1.MagneticPole == magnet2.MagneticPole)
            force = -force;

        return (force / Time.fixedDeltaTime) * r.normalized;
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

                if (magnets[i].transform.parent == magnets[j].transform.parent)
                    continue;

                desiredForce += CalculateGilbertForce(magnets[i], magnets[j]);
            }

            desiredForce = Vector3.ClampMagnitude(desiredForce, MaxForce);

            if (desiredForce.magnitude > 0)
            {
                Debug.Log(magnets[i]);
                Debug.Log(desiredForce);
            }

            magnets[i].rigidbody.AddForceAtPosition(desiredForce, magnets[i].transform.position);
        }
    }

    void OnDrawGizmos()
    {
        var magnets = FindObjectsOfType<Magnet>();

        for (int i = 0; i < magnets.Length; i++)
        {
            var m1 = magnets[i];           

            for (int j = 0; j < magnets.Length; j++)
            {
                var m2 = magnets[j];

                if (m1 == m2)
                    continue;

                if (m2.MagnetForce < 5.0f)
                    continue;

                if (m1.transform.parent == m2.transform.parent)
                    continue;

                var f = CalculateGilbertForce(m1, m2);

                if (m2.MagneticPole)
                {
                    Gizmos.color = Color.cyan;
                }
                else
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawLine(m1.transform.position, m1.transform.position + f);
            }
        }
    }

}
