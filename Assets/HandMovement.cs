using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HandMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_handCurve;
    [SerializeField] private float m_speed = 0.2f;
    [SerializeField] private Vector2 m_scale = Vector2.one;
    private VisualEffect m_effect;
    private float m_time;

    private void Start()
    {
        m_effect = GetComponentInChildren<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        m_time = (m_time + Time.deltaTime * m_speed) % 1f;
        m_effect.SetVector3("SpawnPos", this.transform.position);
        transform.position = new Vector3(m_handCurve.Evaluate(m_time) * m_scale.x, 0, m_time * m_scale.y);
    }
}
