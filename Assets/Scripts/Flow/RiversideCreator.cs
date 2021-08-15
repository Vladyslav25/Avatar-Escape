using System;
using N_Enum;
using N_Library;
using N_Pool;
using UnityEngine;

public class RiversideCreator : GenericSingleton<RiversideCreator>
{
	[SerializeField]
	private ScriptableScoreData m_scoreData;
	
	private RiverSide m_prefabLeft;
	private RiverSide m_prefabRight;

	private void Start()
	{
		// MeshRenderer r;
		// r.bounds
		
		m_prefabLeft = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);
		m_prefabRight = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);

		m_prefabLeft.transform.position = new Vector3(-ObstacleCreator.Instance.RiverHalfWidth, 0, 0);
		m_prefabLeft.gameObject.SetActive(true);
		m_prefabRight.transform.position = new Vector3(ObstacleCreator.Instance.RiverHalfWidth, 0, 0);
		m_prefabRight.gameObject.SetActive(true);
	}
}
