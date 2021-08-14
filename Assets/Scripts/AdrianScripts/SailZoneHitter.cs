using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailZoneHitter : MonoBehaviour, IHitable
{
    public event System.Action<SailZoneHitter, Vector3, float> OnHitEvent;

    public void GotHit(Vector3 hitPosition, float bulletStrength)
    {
        Debug.Log($"{this.transform.name} got hit");
        OnHitEvent?.Invoke(this, hitPosition, bulletStrength);
    }
}
