using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventHitter : MonoBehaviour, IHitable
{
    [SerializeField]
    private UnityEvent m_HitEvent;

    private Rigidbody m_rigidbody = null;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void GotHit(Vector3 hitPosition, float bulletStrength)
    {
        if(m_rigidbody != null)
        {
            m_rigidbody.AddExplosionForce(bulletStrength * 10.0f, hitPosition, 10.0f);
        }

        m_HitEvent.Invoke();
    }
}
