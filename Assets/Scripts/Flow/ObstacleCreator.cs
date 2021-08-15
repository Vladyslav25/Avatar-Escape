using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using N_Library;
using N_Enum;
using N_Pool;

public class ObstacleCreator : GenericSingleton<ObstacleCreator>
{
	public int ObstaclesInMotion
	{
		get => m_obstaclesInMotion;
		set
		{
			m_obstaclesInMotion = value;

			if (m_obstaclesInMotion == m_maximumObstacles)
			{
				m_coroutineIsRunning = false;
				m_counter = 0;
			}
			else if (!m_coroutineIsRunning)
			{
				StartCoroutine(GenerateObstacles());
			}
		}
	}
	public int MaximumObstacles { get => m_maximumObstacles; set => m_maximumObstacles = value; }
	public float RiverEnd => m_riverEnd;
	public float RiverHalfWidth => (m_instantiateRange + m_riverSideOffset) * .5f;

	[SerializeField]
	private ScriptableScoreData m_scoreData;
	
	[Header("River")]
	[SerializeField]
	private float m_riverStart = 20;
	[SerializeField]
	private float m_riverEnd = -20;
	[SerializeField]
	private float m_instantiateRange = 10;
	[SerializeField]
	private float m_riverSideOffset = 2;

	[Header("Obstacles")]
	[SerializeField]
	private int m_maximumObstacles = 10;
	[SerializeField]
	private float m_creationDelay = 2;
	[SerializeField]
	private float m_minimumSpeed = 3;
	[SerializeField]
	private float m_maximumSpeed = 10;
	[SerializeField]
	private Vector3 m_minimumRotation = new Vector3(0, -180, 0);
	[SerializeField]
	private Vector3 m_maximumRotation = new Vector3(0, 180, 0);
	
	[Header("Random details")]
	[SerializeField]
	private int m_maxLastPositions = 5;
	[SerializeField]
	private float m_lastPositionThreshold = 0.05f;

	private readonly Queue<float> m_lastPositionValues = new Queue<float>();
	private bool m_coroutineIsRunning;
	private int m_obstaclesInMotion;
	private float m_counter;
	private readonly System.Random m_rnd = new System.Random();

	private void OnValidate()
	{
		if (m_lastPositionThreshold > .5f)
		{
			m_lastPositionThreshold = .5f;
		}

		m_scoreData.AverageSpeed = Mathf.Lerp(m_minimumSpeed, m_maximumSpeed, .5f);
	}

	private void Start()
	{
		StartCoroutine(GenerateObstacles());
	}

	private IEnumerator GenerateObstacles()
	{
		m_coroutineIsRunning = true;

		while (m_coroutineIsRunning)
		{
			m_counter += Time.deltaTime;

			if (m_counter >= m_creationDelay)
			{
				EItem item = (EItem)m_rnd.Next(0, PoolControl.Instance.Prefabs.Length);
				Obstacle obst = PoolControl.Instance.GetItem(item);
				SetRandomPosition(obst.transform);
				obst.Speed = GetRandomSpeed();
				obst.transform.localEulerAngles = GetRandomRotation();
				obst.SetVelocity();

				ObstaclesInMotion++;
				m_counter = 0;
			}

			yield return null;
		}
	}

	private void SetRandomPosition(Transform _transform)
	{		
		float right = m_instantiateRange * .5f;
		float left = -right;
		float t;

		while (true)
		{
			t = (float)m_rnd.NextDouble();
			t = LastPositionCheck(t, out bool leave);

			if (leave)
				break;
		}
		
		float posX = Mathf.Lerp(left, right, t);

		_transform.position = new Vector3(posX, _transform.position.y, m_riverStart);
	}

	private float LastPositionCheck(float _value, out bool _leave)
	{
		if (m_lastPositionValues.Count > 0)
		{
			_leave = !m_lastPositionValues.Any(_f => Mathf.Abs( Mathf.Abs(_f) - Mathf.Abs(_value) ) < m_lastPositionThreshold);
			
			if (m_lastPositionValues.Count >= m_maxLastPositions)
			{
				m_lastPositionValues.Dequeue();
				m_lastPositionValues.Enqueue(_value);
			}
			else
			{
				m_lastPositionValues.Enqueue(_value);
			}
			
			return _value;
		}
		
		m_lastPositionValues.Enqueue(_value);
			
		_leave = true;			
		return _value;
	}

	private float GetRandomSpeed()
	{
		float t = (float)m_rnd.NextDouble();
		return Mathf.Lerp(m_minimumSpeed, m_maximumSpeed, t);
	}

	private Vector3 GetRandomRotation()
	{
		float t = (float)m_rnd.NextDouble();
		return Vector3.Lerp(m_minimumRotation, m_maximumRotation, t);
	}
}
