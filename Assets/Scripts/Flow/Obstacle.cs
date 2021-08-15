using System;
using N_Enum;
using UnityEngine;
using N_Pool;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
	public float Speed { get => m_speed; set => m_speed = value; }

	[SerializeField]
	private EItem m_type;
	private float m_speed;
	private Rigidbody m_rb;

	private void Awake()
	{
		m_rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		BorderCheck();
	}

	private void OnDisable()
	{
		ReturnItem();
	}

	public void SetVelocity()
	{
		m_rb.velocity = -Vector3.forward * m_speed;
	}

	private void BorderCheck()
	{
		// end check
		if (transform.position.z <= ObstacleCreator.Instance.RiverEnd)
		{
			gameObject.SetActive(false);
		}
		// side check
		else if (transform.position.x <= -ObstacleCreator.Instance.RiverHalfWidth ||
				 transform.position.x >= ObstacleCreator.Instance.RiverHalfWidth )
		{
			gameObject.SetActive(false);
		}
	}

	private void ReturnItem()
	{
		if (ObstacleCreator.Instance != null)
			ObstacleCreator.Instance.ObstaclesInMotion--;
		
		PoolControl.Instance.ReturnItem(m_type, this);
	}
}
