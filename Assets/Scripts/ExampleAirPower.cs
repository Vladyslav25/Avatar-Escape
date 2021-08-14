using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExampleAirPower : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1)] private float m_chragedPower;
    [SerializeField] private VisualEffect m_effect;
    int m_effectWaveId;
    private void Start()
    {
        m_effectWaveId = Shader.PropertyToID("ChargedPower");
    }

    private void Update()
    {
        m_effect.SetFloat(m_effectWaveId, m_chragedPower);
    }
}
