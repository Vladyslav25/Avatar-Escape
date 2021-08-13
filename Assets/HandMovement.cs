using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_handCurve;
    [SerializeField] private float m_speed = 0.2f;
    [SerializeField] private Vector2 m_scale = Vector2.one;
    private float m_time;

    // Update is called once per frame
    void Update()
    {
        m_time = (m_time + Time.deltaTime * m_speed) % 1f;
        transform.position = new Vector3(m_handCurve.Evaluate(m_time) * m_scale.x, 0, m_time * m_scale.y);
    }
}
