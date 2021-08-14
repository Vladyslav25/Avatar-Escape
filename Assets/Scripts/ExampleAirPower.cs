using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ExampleAirPower : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1)] private float m_chragedPower;
    [SerializeField] private bool m_isActiveCharging;
    [SerializeField] private VisualEffect m_effect;
    private int m_isActive;
    private int m_effectWaveId;
    private void Start()
    {
        m_effectWaveId = Shader.PropertyToID("ChargedPower");
        m_isActive = Shader.PropertyToID("KeepSpawning");
    }

    private void Update()
    {
        m_effect.SetFloat(m_effectWaveId, m_chragedPower);
        ChangeActive = m_isActiveCharging;
    }

    public bool ChangeActive
    {
        get => m_effect.GetBool(m_isActive);
        set => m_effect.SetBool(m_isActive, value);
    }
}
