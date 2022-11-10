using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalParent : MonoBehaviour
{
    public float minMagnetizedDistance = 5;

    public GameObject southPole;
    public GameObject northPole;

    private void FixedUpdate()
    {
        UpdateMagnetized();
    }

    private void UpdateMagnetized()
    {
        Magnet magneticPole = MagnetObject.FindClosestMagneticPole(this.gameObject);
        if (magneticPole == null)
            return;

        if (Vector3.Distance(magneticPole.transform.position, transform.position) > minMagnetizedDistance * magneticPole.MagnetForce)
        {
            southPole.SetActive(false);
            northPole.SetActive(false);
            return;
        }

        Vector3 opposingPolePoint = gameObject.GetComponent<Collider>().ClosestPoint(magneticPole.transform.position);
        Vector3 samePolePointPoint = transform.TransformPoint(transform.InverseTransformPoint(opposingPolePoint) * -1f);

        float magnetForce = magneticPole.MagnetForce * (1 / (1 + Mathf.Pow(Vector3.Distance(magneticPole.transform.position, opposingPolePoint), 2)));

        GameObject[] poles = new GameObject[2] { southPole, northPole };
        if (magneticPole.magneticPole == MagneticPole.north)
            poles = new GameObject[2] { northPole, southPole };


        poles[0].transform.position = samePolePointPoint;
        poles[1].transform.position = opposingPolePoint;

        poles[0].GetComponent<Magnet>().MagnetForce = 0.8f * magnetForce;
        poles[1].GetComponent<Magnet>().MagnetForce = 0.9f * magnetForce;

        southPole.SetActive(true);
        northPole.SetActive(true);
    }
}
