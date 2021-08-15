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

	private MeshRenderer m_meshRenderer;

	protected override void Awake()
	{
		base.Awake();
	}

	private void Start()
	{		
		m_meshRenderer = LevelGeoPool.Instance.Prefabs[0].Item.GetComponent<MeshRenderer>();
		
		m_prefabLeft = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);
		m_prefabRight = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);

		float geoHalfExtends = m_meshRenderer.bounds.extents.x;
		m_prefabLeft.transform.position = new Vector3(-ObstacleCreator.Instance.RiverHalfWidth - geoHalfExtends, 0, 0);
		m_prefabLeft.gameObject.SetActive(true);
		m_prefabRight.transform.position = new Vector3(ObstacleCreator.Instance.RiverHalfWidth + geoHalfExtends, 0, 0);
		m_prefabRight.gameObject.SetActive(true);
	}
}
