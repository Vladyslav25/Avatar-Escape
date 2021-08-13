using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable 
{
    public void GotHit(Vector3 hitPosition, float bulletStrength);
}
