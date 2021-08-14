using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

public class EnemyCatapult : MonoBehaviour
{
    [SerializeField]
    private float m_TimeBetweenSpawn;

    [SerializeField]
    private float m_minRandomSpawnTimeAddition = 0.5f;

    [SerializeField]
    private float m_maxRandomSpawnTimeAddition = 5.0f;

    private float m_nextSpawn;

    [SerializeField]
    private Rigidbody[] m_Enemies;

    [SerializeField]
    private Transform m_Launcher; 

    [SerializeField]
    private Transform m_TargetPoint;

    [SerializeField]
    private float m_ZVariation = 0.5f;

    [SerializeField]
    private float m_xVariation = 1.0f;

    private Rigidbody m_currentFlyingEnemy;

    [SerializeField]
    private float m_launchAngle = 70.0f;

    private void Awake()
    {
        m_nextSpawn =Time.time + m_TimeBetweenSpawn + Random.Range(m_minRandomSpawnTimeAddition, m_maxRandomSpawnTimeAddition);
    }

    private void Update()
    {
        CheckForSpawn();
    }


    private void CheckForSpawn()
    {
        if(Time.time > m_nextSpawn)
        {
            int id = Random.Range(0, m_Enemies.Length);

            m_currentFlyingEnemy = Instantiate(m_Enemies[id], m_Launcher.position, m_Enemies[id].transform.rotation);


            Vector3 velo = GetVelocity();
            // launch the object by setting its initial velocity and flipping its state
            m_currentFlyingEnemy.velocity = velo;

            m_nextSpawn = Time.time + m_TimeBetweenSpawn + Random.Range(m_minRandomSpawnTimeAddition, m_maxRandomSpawnTimeAddition);
        }
    }

    private Vector3 GetVelocity()
    {
        float xvar = Random.Range(-m_xVariation, m_xVariation);
        float zvar = Random.Range(-m_ZVariation, m_ZVariation);

        Vector3 targetPoint = m_TargetPoint.position + new Vector3(xvar, 0.0f, zvar);

        Vector3 projectileXZPos = new Vector3(m_Launcher.position.x, 0.0f, m_Launcher.position.z);
        Vector3 targetXZPos = new Vector3(targetPoint.x, m_Launcher.position.y, targetPoint.z);

        // rotate the object to face the target
        m_Launcher.LookAt(targetXZPos);

        // shorthands for the formula
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(m_launchAngle * Mathf.Deg2Rad);
        float H = targetPoint.y - m_Launcher.position.y;

        // calculate the local space components of the velocity 
        // required to land the projectile on the target object 
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        // create the velocity vector in local space and get it in global space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = m_Launcher.TransformDirection(localVelocity);

        return globalVelocity;
    }

    private void OnDrawGizmos()
    {
        if(m_TargetPoint == null)
        {
            return;
        }

        Gizmos.color = Color.red;

        Vector3 pointone = new Vector3(-m_xVariation, 0.0f, -m_ZVariation) + m_TargetPoint.position; 
        Vector3 pointtwo = new Vector3(m_xVariation, 0.0f, -m_ZVariation) + m_TargetPoint.position; 
        Vector3 pointthree = new Vector3(m_xVariation, 0.0f, m_ZVariation) + m_TargetPoint.position; 
        Vector3 pointfour = new Vector3(-m_xVariation, 0.0f, m_ZVariation) + m_TargetPoint.position;

        Gizmos.DrawLine(pointone, pointtwo);
        Gizmos.DrawLine(pointtwo, pointthree);
        Gizmos.DrawLine(pointthree, pointfour);
        Gizmos.DrawLine(pointfour, pointone);
    }
}
