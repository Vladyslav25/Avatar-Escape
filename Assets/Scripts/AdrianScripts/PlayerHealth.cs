using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int m_maxHealth = 6;

    private int m_currentHealth;

    [SerializeField]
    private Flagswitch m_LifeFlag;

    private void Awake()
    {
        m_currentHealth = m_maxHealth;
    }

    public void BoatGotDamage(int amount)
    {
        m_currentHealth -= amount;

        m_currentHealth = m_currentHealth < 0 ? 0 : m_currentHealth;

        m_LifeFlag.HP = m_currentHealth;

        CheckForLose();
    }

    private void CheckForLose()
    {
        if (m_currentHealth > 0)
        {
            return;
        }

        GameManager.Instance.GameLost();

    }

    public void BoatGotHealed(int amount)
    {
        m_currentHealth += amount;

        m_currentHealth = m_currentHealth > m_maxHealth ? m_maxHealth : m_currentHealth;

        m_LifeFlag.HP = m_currentHealth;
    }
}
