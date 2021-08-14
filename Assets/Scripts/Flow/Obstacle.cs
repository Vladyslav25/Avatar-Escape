using System;
using N_Enum;
using UnityEngine;
using N_Pool;

public class Obstacle : MonoBehaviour
{
	public float Speed { get => m_speed; set => m_speed = value; }

	[SerializeField]
	private EItem m_type;
	private float m_speed;
	private Rigidbody m_rb;
	private bool m_onDisableIsActive;

	private void Awake()
	{
		m_rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		BorderCheck();
	}

	public void SetVelocity()
	{
		m_rb.velocity = -transform.forward * m_speed;
	}

	private void BorderCheck()
	{
		// end check
		if (transform.position.z <= ObstacleCreator.Instance.RiverEnd)
		{
			ReturnItem();
		}
		// side check
		else if (transform.position.x <= -ObstacleCreator.Instance.RiverHalfWidth ||
				 transform.position.x >= ObstacleCreator.Instance.RiverHalfWidth )
		{
			ReturnItem();
		}
	}

	public void ReturnItem()
	{
		if (ObstacleCreator.Instance != null)
			ObstacleCreator.Instance.ObstaclesInMotion--;
		
		PoolControl.Instance.ReturnItem(m_type, this);
	}
}
