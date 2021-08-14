using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BootController : MonoBehaviour
{
    [SerializeField]
    private SailZoneHitter m_LeftSailZone;

    [SerializeField]
    private SailZoneHitter m_MidSailZone;

    [SerializeField]
    private SailZoneHitter m_RightSailZone;

    private Rigidbody m_rigidody;

    private void Awake()
    {
        m_rigidody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        m_MidSailZone.OnHitEvent += ZoneGotHit;
        m_LeftSailZone.OnHitEvent += ZoneGotHit;
        m_RightSailZone.OnHitEvent += ZoneGotHit;

    }

    private void ZoneGotHit(SailZoneHitter zone, Vector3 hitPosition, float bulletStrength)
    {
        if(zone == m_MidSailZone)
        {
            Debug.Log("mid");

            MidZoneGotHit(hitPosition, bulletStrength);
            return;
        }

        if(zone == m_LeftSailZone)
        {
            Debug.Log("left");
            LeftZoneGotHit(hitPosition, bulletStrength);
            return;
        }

        if (zone == m_RightSailZone)
        {
            Debug.Log("right");

            RightZoneGotHit(hitPosition, bulletStrength);
            return;
        }

        Debug.Log("Why are you here?");
    }

    private void MidZoneGotHit(Vector3 hitPosition, float bulletStrength)
    {
        Debug.Log("Faster!");
    }

    private void LeftZoneGotHit(Vector3 hitPosition, float bulletStrength)
    {
        m_rigidody.AddForce(this.transform.right * bulletStrength * 100);
    }

    private void RightZoneGotHit(Vector3 hitPosition, float bulletStrength)
    {
        m_rigidody.AddForce(this.transform.right * -bulletStrength * 100);
    }
}
