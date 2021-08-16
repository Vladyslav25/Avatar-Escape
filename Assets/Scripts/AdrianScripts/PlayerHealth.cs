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

    [SerializeField]
    private AK.Wwise.Event m_FullLifeEvent;

    //[SerializeField]
    //private AK.Wwise.Event m_StopNormalLife;
    [SerializeField]
    private AK.Wwise.Event m_midLifeEvent;

    //[SerializeField]
    //private AK.Wwise.Event m_StopMidLide;
    [SerializeField]
    private AK.Wwise.Event m_lowLifeEvent;

    private void Awake()
    {
        m_currentHealth = m_maxHealth;
        _ = m_FullLifeEvent.Post(this.gameObject);
    }

    [ContextMenu("DamageBoat")]
    private void DamageDebug()
    {
        BoatGotDamage(1);
    }

    public void BoatGotDamage(int amount)
    {

        int newLife = m_currentHealth - amount;

        if (newLife > 2 && newLife <= 4)
        {
            if (m_currentHealth > 4)
            {
                Debug.Log("MId");
                //_ = m_StopNormalLife.Post(this.gameObject);
                // _ = m_midLifeEvent.Post(this.gameObject);
            }
        }

        else if (newLife > 0 && newLife <= 2)
        {
            if (m_currentHealth > 2)
            {
                Debug.Log("LOw");
                //_ = m_StopMidLide.Post(this.gameObject);
                //_ = m_lowLifeEvent.Post(this.gameObject);
            }
        }

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
