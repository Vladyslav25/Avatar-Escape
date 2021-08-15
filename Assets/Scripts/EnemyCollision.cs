using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    public AK.Wwise.Event enemyCollisionEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boat")
        {
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.None;
            enemyCollisionEvent.Post(gameObject);
        }
    }
}
