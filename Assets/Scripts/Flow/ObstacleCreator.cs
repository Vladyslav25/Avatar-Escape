using System.Collections;
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
	private float m_maximumSpeed = 10;
	[SerializeField]
	private float m_minimumSpeed = 3;

	private bool m_coroutineIsRunning;
	private int m_obstaclesInMotion;
	private float m_counter;
	private readonly System.Random m_rnd = new System.Random();

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
				EItem item = (EItem)m_rnd.Next(0, 2);
				Obstacle obst = PoolControl.Instance.GetItem(item);
				SetRandomPosition(obst.transform);
				obst.Speed = GetRandomSpeed();
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

		float t = (float)m_rnd.NextDouble();
		float posX = Mathf.Lerp(left, right, t);

		_transform.position = new Vector3(posX, _transform.position.y, m_riverStart);
	}
	
	private float GetRandomSpeed()
	{
		float t = (float)m_rnd.NextDouble();
		return Mathf.Lerp(m_minimumSpeed, m_maximumSpeed, t);
	}
}
