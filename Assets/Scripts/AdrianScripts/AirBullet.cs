using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AirBullet : MonoBehaviour
{
    [SerializeField]
    private float m_LifeTime = 30.0f;

    private float m_ReleaseTime = 0.0f;

    private Rigidbody m_RigidBody = null;

    private bool m_IsReleased = false;

    [SerializeField]
    private float m_maxStrength = 10.0f;

    [SerializeField]
    private float m_maxMass = 10.0f;

    [SerializeField]
    private Vector3 m_maxSize = Vector3.one;

    private float m_currentStrength;
    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();

        m_RigidBody.isKinematic = true;
    }

    private void Update()
    {
        CheckForDeath(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!m_IsReleased)
        {
            return; 
        }

        IHitable hit = collision.transform.GetComponent<IHitable>();

        if(hit != null)
        {
            hit.GotHit(collision.GetContact(0).point, m_currentStrength);
        }


        Destroy(this.gameObject);
    }

    private void CheckForDeath()
    {
        if (!m_IsReleased)
        {
            return;
        }

        if(Time.time > m_ReleaseTime + m_LifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetSize(float size)
    {
        m_currentStrength = m_maxStrength * size;
        this.transform.localScale = m_maxSize * size;
        m_RigidBody.mass = m_maxMass * size;
    }

    public void RealeaseBullet(Vector3 velocity)
    {
        this.transform.parent = null;

        m_ReleaseTime = Time.time;

        m_IsReleased = true;

        m_RigidBody.isKinematic = false;

        m_RigidBody.velocity = velocity;
    }
    public void SetUp(Transform newParent)
    {
        this.transform.localPosition = Vector3.zero;
    }
}
