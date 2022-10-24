using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetParent : MonoBehaviour
{
    public float MagnetForce;
    Magnet[] m_magnets;

    void Start()
    {
        m_magnets = GetComponentsInChildren<Magnet>();
    }

    void Update()
    {
        for (int i = 0; i < m_magnets.Length; i++)
        {
            m_magnets[i].MagnetForce = MagnetForce;
        }
    }

    //find nearest magnetic pole, increase magnet force y = 1/x^2
}
