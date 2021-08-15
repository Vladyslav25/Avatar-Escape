using N_Enum;
using N_Pool;
using UnityEngine;

public class RiverSide : MonoBehaviour
{
    [SerializeField]
    private ELevelGeo m_type;

    

    private float m_riverEnd;
    public void Init(float _riverEnd, int _startLater = 0)
    {
        m_riverEnd = _riverEnd;
        if (_startLater >= 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 80 * _startLater);
        }
    }

    private float GetNextZPos => transform.position.z - Time.deltaTime * RiversideCreator.Instance.GetRiverSideSpeed;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, GetNextZPos);
        if (transform.position.z <= m_riverEnd * 2)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + m_riverEnd * -4);
            //LevelGeoPool.Instance.ReturnItem(ELevelGeo.RiverSide, this);
            //this.gameObject.SetActive(false);
        }
    }
}
