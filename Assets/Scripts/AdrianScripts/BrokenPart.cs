using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPart : MonoBehaviour
{

    [SerializeField]
    private float m_lifeTime = 20.0f;

    private Transform m_startParent;

    private Vector3 m_startLocalPosition;

    private Vector3 m_StartLocalRotation;

    private float m_startTime = float.MaxValue;

    private Rigidbody m_Rigidbody;

    //private void Awake()
    //{
    //    m_startTime = Time.time;

    //    m_startParent = this.transform.parent;

    //    m_startLocalPosition = this.transform.localPosition;
    //    m_StartLocalRotation = this.transform.localEulerAngles;

    //    if (m_Rigidbody == null)
    //        m_Rigidbody = GetComponent<Rigidbody>();
    //}

    private void OnEnable()
    {
        m_startTime = Time.time;

        m_startParent = this.transform.parent;

        m_startLocalPosition = this.transform.localPosition;
        m_StartLocalRotation = this.transform.localEulerAngles;

        if (m_Rigidbody == null)
            m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.time > m_startTime + m_lifeTime)
        {
            ResetToParent();
        }
    }

    private void ResetToParent()
    {
        this.transform.parent = m_startParent;
        this.transform.localPosition = m_startLocalPosition;
        this.transform.localEulerAngles = m_StartLocalRotation;

        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;

        m_startTime = float.MaxValue;
    }
}
