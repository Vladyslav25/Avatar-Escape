using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwimHitter : MonoBehaviour, IHitable
{
    [SerializeField]
    private float m_halfBrokenDamage = 3.0f;

    private bool m_isHalfBroken = false;

    [SerializeField]
    private float m_FullyBrokenDamage = 6.0f;

    private float m_currentDamage = 0.0f;

    [SerializeField]
    GameObject m_UnBrokenObject;

    [SerializeField]
    Rigidbody[] m_HalfBrokeParts;

    [SerializeField]
    GameObject m_HalfBrokenObject;


    [SerializeField]
    Rigidbody[] m_FullBrokenParts;

    [SerializeField]
    GameObject m_FullyBrokenObject;

    [SerializeField]
    private float m_ExplosionMultiplier = 100.0f;

    [SerializeField]
    private bool m_IsAHealingObject;


    [SerializeField]
    private float m_pingPongMinMaxValue;

    private float m_randomTimeAdder = 0;
    
    [SerializeField]
    private AK.Wwise.Event barrellCollisionEvent;

    private void Awake()
    {
        m_randomTimeAdder = Random.Range(0, 10.0f);
    }

    private void Update()
    {


        float newY = Mathf.PingPong((Time.time + m_randomTimeAdder) / 2 , m_pingPongMinMaxValue);

        Vector3 local = this.transform.localPosition;

        local.y = newY;

        this.transform.position = local;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Boat"))
        {
            PlayerHealth health = collision.transform.GetComponent<PlayerHealth>();


            if (m_IsAHealingObject)
            {
                health.BoatGotDamage(1);
            }
            else
            {
                int dmg = m_isHalfBroken ? 1 : 2;
                health.BoatGotDamage(dmg);
            }

            GotHit(this.transform.position, m_FullyBrokenDamage + 1);
        }

        else if(collision.transform.CompareTag("Boundary"))
        {
            GotHit(this.transform.position, m_FullyBrokenDamage + 1);
        }
    }

    public void GotHit(Vector3 hitPosition, float bulletStrength)
    {
        m_currentDamage += bulletStrength;

        if (!m_isHalfBroken && m_currentDamage > m_halfBrokenDamage)
        {
            BrakeObjectHalf(hitPosition, bulletStrength);
        }

        if (m_currentDamage > m_FullyBrokenDamage)
        {
            BrakeObjectCompletly(hitPosition, bulletStrength);
        }
    }

    private void BrakeObjectHalf(Vector3 hitPosition, float bulletStrength)
    {
        m_UnBrokenObject.SetActive(false);
        m_HalfBrokenObject.SetActive(true);

        foreach (Rigidbody rb in m_HalfBrokeParts)
        {
            rb.transform.parent = null;
            rb.AddExplosionForce(bulletStrength * m_ExplosionMultiplier, hitPosition, 10.0f);
        }

        barrellCollisionEvent.Post(this.gameObject);
    }

    private void BrakeObjectCompletly(Vector3 hitPosition, float bulletStrength)
    {
        m_HalfBrokenObject.SetActive(false);
        m_FullyBrokenObject.SetActive(true);

        foreach (Rigidbody rb in m_FullBrokenParts)
        {
            rb.transform.parent = null;
            rb.AddExplosionForce(bulletStrength * m_ExplosionMultiplier, hitPosition, 10.0f);
        }


        m_FullyBrokenObject.SetActive(false);
        m_UnBrokenObject.SetActive(true);

        this.gameObject.SetActive(false);
        m_currentDamage = 0.0f;

        barrellCollisionEvent.Post(this.gameObject);
    }
}
