using N_Library;
using UnityEngine;

public class TrackDistanceCounter : GenericSingleton<TrackDistanceCounter>
{
	[SerializeField]
	private ScriptableScoreData m_scoreData; 
	
	private System.DateTime m_startTime;
	private System.DateTime m_endTime;
	
	protected override void Awake()
	{
		base.Awake();
		m_startTime = System.DateTime.UtcNow;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		
		m_endTime = System.DateTime.UtcNow;
		System.TimeSpan timeSpan = m_endTime.Subtract(m_startTime);

		m_scoreData.Score = timeSpan.Seconds * m_scoreData.AverageSpeed;
		Debug.Log($"Score: {m_scoreData.Score}");
	}
}
