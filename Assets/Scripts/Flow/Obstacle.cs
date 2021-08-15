using System;
using System.Collections;
using UnityEngine;
using N_Pool;
using N_Library;
using N_Enum;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
	public float Speed { get => m_speed; set => m_speed = value; }
	public bool BorderReached { get => m_borderReached; set => m_borderReached = value; }
	public Vector3 StartRotation { get => m_startRotation; set => m_startRotation = value; }

	[SerializeField]
	private EItem m_type;
	[SerializeField]
	private float m_fadeTime = 2;
	[SerializeField]
	private float m_uniformFadeMinimumScale = 0;
	[SerializeField]
	private float m_uniformFadeMaximumScale = 1;

	[Header("Wiggle")]
	[SerializeField]
	private float m_wiggleDistance = 1;
	[SerializeField]
	private float m_wiggleSpeed = 1;
	[SerializeField]
	private Vector3 m_minimumRotation = new Vector3(0, -180, 0);
	[SerializeField]
	private Vector3 m_maximumRotation = new Vector3(0, 180, 0);

	private Vector3 m_fadeMinimumScale;
	private Vector3 m_fadeMaximumScale;
	private Vector3 m_startRotation;

	private System.Random m_rnd = new System.Random();
	private int m_direction;
	private Rigidbody m_rb;
	private float m_speed;
	private float m_counter;
	private bool m_borderReached;

	private void Awake()
	{
		m_rb = GetComponent<Rigidbody>();

		Vector3 scale = transform.localScale;
		m_fadeMinimumScale = scale * m_uniformFadeMinimumScale;
		m_fadeMaximumScale = scale * m_uniformFadeMaximumScale;
	}

	private void OnEnable()
	{
		StartCoroutine(Fade(FadeSet.In));
		m_direction = (int)Mathf.Lerp(-1, 1, m_rnd.Next(0, 2));
	}

	private void Update()
	{
		RotationWiggle();
		BorderCheck();
	}

	private void OnDisable()
	{
		RotationWiggle();
		ReturnItem();
	}

	public void SetVelocity()
	{
		m_rb.velocity = -Vector3.forward * m_speed;
	}

	private void RotationWiggle()
	{
		float wiggle = Mathf.Cos(Time.time * m_wiggleSpeed) * m_wiggleDistance;
		wiggle *= m_direction;

		if (wiggle >= 0)
		{
			transform.localEulerAngles = Vector3.Lerp(StartRotation, m_maximumRotation, Mathf.Abs(wiggle));
		}
		else
		{
			transform.localEulerAngles = Vector3.Lerp(StartRotation, m_minimumRotation, Mathf.Abs(wiggle));
		}
	}

	private void BorderCheck()
	{
		if (!m_borderReached)
		{
			// end check
			if (transform.position.z <= ObstacleCreator.Instance.RiverEnd)
			{
				StartCoroutine(Fade(FadeSet.Out));
				m_borderReached = true;
			}
			// side check
			else if (transform.position.x <= -ObstacleCreator.Instance.RiverHalfWidth || transform.position.x >= ObstacleCreator.Instance.RiverHalfWidth)
			{
				StartCoroutine(Fade(FadeSet.Out));
				m_borderReached = true;
			}
		}
	}

	private void ReturnItem()
	{
		if (ObstacleCreator.Instance != null)
			ObstacleCreator.Instance.ObstaclesInMotion--;

		ItemPool.Instance.ReturnItem(m_type, this);
	}

	private IEnumerator Fade(FadeSet _fade)
	{
		while (true)
		{
			Transform t = transform;
			m_counter += Time.deltaTime;

			float percent = m_counter.GetPercentage(0, m_fadeTime);

			t.localScale = _fade switch
			{
				FadeSet.Out => Vector3.Lerp(m_fadeMaximumScale, m_fadeMinimumScale, percent),
				FadeSet.In => Vector3.Lerp(m_fadeMinimumScale, m_fadeMaximumScale, percent),
				_ => t.localScale
			};

			if (m_counter >= m_fadeTime)
			{
				m_counter = 0;

				switch (_fade)
				{
					case FadeSet.Out:
						gameObject.SetActive(false);
						break;
					case FadeSet.In:
						yield break;
				}
			}

			yield return null;
		}

		// ReSharper disable once IteratorNeverReturns
	}
}
