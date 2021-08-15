using UnityEngine;

[CreateAssetMenu(fileName = "Score data",menuName = "Scriptable objects/Score data")]
public class ScriptableScoreData : ScriptableObject, ISerializationCallbackReceiver
{
	public float Score { get => m_score; set => m_score = value; }
	public float AverageSpeed { get => m_averageSpeed; set => m_averageSpeed = value; }

	private float m_score;
	private float m_averageSpeed;
	
	public void OnBeforeSerialize()
	{
	}
	
	public void OnAfterDeserialize()
	{
		m_score = 0;
		m_averageSpeed = 0;
	}
}
