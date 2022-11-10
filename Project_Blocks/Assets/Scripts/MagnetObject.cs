using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetObject : MonoBehaviour
{
    public readonly static HashSet<Magnet> Pool = new HashSet<Magnet>();

    private void Start()
    {
        Magnet[] magnets = FindObjectsOfType<Magnet>();
        Pool.UnionWith(magnets);
    }

    public static Magnet FindClosestMagneticPole(GameObject obj)
    {
        Magnet result = null;
        Vector3 pos = obj.transform.position;
        float dist = float.PositiveInfinity;
        var e = Pool.GetEnumerator();

        while (e.MoveNext())
        {
            float d = (e.Current.transform.position - pos).sqrMagnitude / e.Current.MagnetForce;
            if (d < dist && !e.Current.gameObject.transform.parent.transform.position.Equals(pos))
            {
                result = e.Current;
                dist = d;
            }
        }
        return result;
    }

    public float MaxForce;

    Vector3 CalculateGilbertForce(Magnet magnet1, Magnet magnet2)
    {
        Vector3 r = magnet2.transform.position - magnet1.transform.position;
        float distance = r.magnitude;

        float force = (magnet1.MagnetForce * magnet2.MagnetForce) / Mathf.Pow(distance, 4f);

        if (magnet1.magneticPole == magnet2.magneticPole)
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

            magnets[i].rigidbody.AddForceAtPosition(desiredForce, magnets[i].transform.position);
        }
    }
}
