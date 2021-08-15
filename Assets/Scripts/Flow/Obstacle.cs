using System.Collections;
using UnityEngine;
using N_Pool;
using N_Library;
using N_Enum;

public class Obstacle : MonoBehaviour
{
	public float Speed { get => m_speed; set => m_speed = value; }
	public bool BorderReached { get => m_borderReached; set => m_borderReached = value; }

	[SerializeField]
	private EItem m_type;
	[SerializeField]
	private float m_fadeTime = 2;
	[SerializeField]
	private float m_uniformFadeMinimumScale = 0;
	[SerializeField]
	private float m_uniformFadeMaximumScale = 1;
	
	private Vector3 m_fadeMinimumScale;
	private Vector3 m_fadeMaximumScale;
	
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
		if (!m_borderReached)
		{
			// end check
			if (transform.position.z <= ObstacleCreator.Instance.RiverEnd)
			{
				StartCoroutine(Fade(FadeSet.Out));
				m_borderReached = true;
			}
			// side check
			else if (transform.position.x <= -ObstacleCreator.Instance.RiverHalfWidth ||
				transform.position.x >= ObstacleCreator.Instance.RiverHalfWidth )
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
		
		PoolControl.Instance.ReturnItem(m_type, this);
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
