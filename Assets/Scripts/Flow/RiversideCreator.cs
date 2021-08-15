using System;
using System.Collections.Generic;
using N_Enum;
using N_Library;
using N_Pool;
using UnityEngine;

public class RiversideCreator : GenericSingleton<RiversideCreator>
{
	[SerializeField]
	private ScriptableScoreData m_scoreData;

    [SerializeField] private float m_riverSideSpeed = 5f;

    private RiverSide m_prefabLeft;
	private RiverSide m_prefabLeft2;
	private RiverSide m_prefabLeft3;
	private RiverSide m_prefabLeft4;
	private RiverSide m_prefabRight;
	private RiverSide m_prefabRight2;
    private RiverSide m_prefabRight3;
    private RiverSide m_prefabRight4;

	private MeshRenderer m_meshRenderer;

    public float GetRiverSideSpeed => m_riverSideSpeed;

    //[SerializeField] private float m_timeToSpawnNextFlora;

    //private float m_spawnTime = 0f;

    //private void Update()
    //{
    //    m_spawnTime += Time.deltaTime;
    //    if (m_spawnTime >= m_timeToSpawnNextFlora)
    //    {
    //        m_spawnTime %= m_timeToSpawnNextFlora;
    //        RiverSide item = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);
    //        item.Init(ObstacleCreator.Instance.RiverEnd);
    //        item.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,
    //            this.transform.position.z - ObstacleCreator.Instance.RiverEnd);
    //    }
    //}

	protected override void Awake()
	{
		base.Awake();
	}

	private void Start()
	{		
		m_meshRenderer = LevelGeoPool.Instance.Prefabs[0].Item.GetComponent<MeshRenderer>();
		
		m_prefabLeft = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);
		m_prefabLeft2 = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);
		m_prefabLeft3 = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);
		m_prefabLeft4 = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);
		m_prefabRight = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);
		m_prefabRight2 = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);
        m_prefabRight3 = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);
        m_prefabRight4 = LevelGeoPool.Instance.GetItem(ELevelGeo.RiverSide);

		float geoHalfExtends = m_meshRenderer.bounds.extents.x;
		m_prefabLeft.transform.position = new Vector3(-ObstacleCreator.Instance.RiverHalfWidth - geoHalfExtends + m_prefabLeft.transform.position.x, 0, 0);
        m_prefabLeft.gameObject.SetActive(true);
		m_prefabRight.transform.position = new Vector3(ObstacleCreator.Instance.RiverHalfWidth + geoHalfExtends + m_prefabRight.transform.position.x -20, 0, 0);
        m_prefabRight.transform.Rotate(Vector3.up, 180, Space.World);
		m_prefabRight.gameObject.SetActive(true);

        m_prefabLeft2.transform.position = new Vector3(-ObstacleCreator.Instance.RiverHalfWidth - geoHalfExtends + m_prefabLeft2.transform.position.x, 0, 0);
        m_prefabLeft2.gameObject.SetActive(true);
        m_prefabRight2.transform.position = new Vector3(ObstacleCreator.Instance.RiverHalfWidth + geoHalfExtends + m_prefabRight2.transform.position.x - 20, 0, 0);
		m_prefabRight2.transform.Rotate(Vector3.up, 180, Space.World);
        m_prefabRight2.gameObject.SetActive(true);

        m_prefabLeft3.transform.position = new Vector3(-ObstacleCreator.Instance.RiverHalfWidth - geoHalfExtends + m_prefabLeft3.transform.position.x, 0, 0);
        m_prefabLeft3.gameObject.SetActive(true);
        m_prefabRight3.transform.position = new Vector3(ObstacleCreator.Instance.RiverHalfWidth + geoHalfExtends + m_prefabRight3.transform.position.x - 20, 0, 0);
        m_prefabRight3.transform.Rotate(Vector3.up, 180, Space.World);
        m_prefabRight3.gameObject.SetActive(true);

        m_prefabLeft4.transform.position = new Vector3(-ObstacleCreator.Instance.RiverHalfWidth - geoHalfExtends + m_prefabLeft4.transform.position.x, 0, 0);
        m_prefabLeft4.gameObject.SetActive(true);
        m_prefabRight4.transform.position = new Vector3(ObstacleCreator.Instance.RiverHalfWidth + geoHalfExtends + m_prefabRight4.transform.position.x - 20, 0, 0);
        m_prefabRight4.transform.Rotate(Vector3.up, 180, Space.World);
        m_prefabRight4.gameObject.SetActive(true);

        m_prefabLeft.Init(ObstacleCreator.Instance.RiverEnd);
		m_prefabRight.Init(ObstacleCreator.Instance.RiverEnd);
        m_prefabLeft2.Init(ObstacleCreator.Instance.RiverEnd, 1);
		m_prefabRight2.Init(ObstacleCreator.Instance.RiverEnd, 1);
	    m_prefabLeft3.Init(ObstacleCreator.Instance.RiverEnd, 2);
		m_prefabRight3.Init(ObstacleCreator.Instance.RiverEnd, 2);
	    m_prefabLeft4.Init(ObstacleCreator.Instance.RiverEnd, 3);
		m_prefabRight4.Init(ObstacleCreator.Instance.RiverEnd, 3);
	}
}
