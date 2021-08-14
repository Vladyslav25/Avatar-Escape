using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HandMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_handCurve;
    [SerializeField] private float m_speed = 0.2f;
    //[SerializeField] private Vector2 m_scale = Vector2.one;
    private VisualEffect m_effect;
    private float m_time;
    private int m_effectWaveId;
    private Rigidbody m_rig;

    private void Start()
    {
        m_effect = GetComponentInChildren<VisualEffect>();
        m_effectWaveId = Shader.PropertyToID("SpawnPos");
        m_rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        m_time = (m_time + Time.fixedDeltaTime * m_speed) % 1f;
        m_effect.SetVector3(m_effectWaveId, this.transform.position);
        //m_rig.MovePosition(new Vector3(m_handCurve.Evaluate(m_time) * m_scale.x, 0, m_time * m_scale.y));

        Vector3 distance = Vector3.zero - transform.position;
        Vector3 normalizedDistance = distance.normalized;

        Vector3 perpendicularDistance = new Vector3(-normalizedDistance.z, 0, normalizedDistance.x);

        m_rig.velocity = perpendicularDistance * m_speed;
    }
}
