using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRPlayerHand : MonoBehaviour
{
    private SteamVR_Behaviour_Pose m_Hand = null;

    [SerializeField]
    private SteamVR_Action_Boolean m_StartBending = null;

    private bool m_BendingInProgress = false;

    private Vector3 m_BendingStartPos = Vector3.zero;

    [SerializeField]
    private float m_WhenToSaveNextPoint = 0.2f;

    private float m_totalArea = 0.0f;

    private Queue<Vector3> m_BendingPositions = new Queue<Vector3>();

    [SerializeField]
    private float m_minNextPointDistance = 0.3f;

    [SerializeField]
    private float m_maxNextPointDistance = 1.2f;


    private float m_SetNextPointTime = 0.0f;

    private void Awake()
    {
        m_Hand = GetComponent<SteamVR_Behaviour_Pose>();
    }
    private void Update()
    {
        CheckForAirBendingStart();
        ChechForAirBendingRunning();

    }

    private void CheckForAirBendingStart()
    {
        if (m_StartBending.GetLastStateDown(m_Hand.inputSource))
        {
            m_BendingInProgress = true;
            m_BendingStartPos = this.transform.localPosition;
            m_SetNextPointTime = Time.time + m_WhenToSaveNextPoint;
        }

        if (m_StartBending.GetLastStateUp(m_Hand.inputSource))
        {
            m_BendingInProgress = false;
            ShootAirBall();
        }
    }

    private void ChechForAirBendingRunning()
    {
        if (m_BendingInProgress)
        {
            if(Time.time < m_SetNextPointTime)
            {
                return;
            }

            m_SetNextPointTime = Time.time + m_WhenToSaveNextPoint;

            Vector3 pos = this.transform.localPosition;

            Debug.Log("Bending");

            if (!IsValidPoint(pos))
            {
                Debug.Log("NotValid");
                return;
            }

            m_BendingPositions.Enqueue(this.transform.localPosition);

            CheckForPossibleArea();
        }
    }

    

    private bool IsValidPoint(Vector3 pos)
    {
        float distToStart = Vector3.Distance(m_BendingStartPos, pos);

        if(distToStart < m_minNextPointDistance || distToStart > m_maxNextPointDistance)
        {
            return false;
        }

        if(m_BendingPositions.Count > 0)
        {
            float distToLastPosition = Vector3.Distance(m_BendingPositions.Peek(), pos);

            if (distToLastPosition < m_minNextPointDistance || distToLastPosition > m_maxNextPointDistance)
            {
                return false;
            }
        }

        return true; 

    }

    private void CheckForPossibleArea()
    {
        if(m_BendingPositions.Count < 2)
        {
            return; 
        }

        Vector3 posOne = m_BendingPositions.Dequeue();
        Vector3 posTwo = m_BendingPositions.Dequeue();

        Debug.DrawLine(m_BendingStartPos, posOne, Color.red, 5.0f);
        Debug.DrawLine(m_BendingStartPos, posTwo, Color.red, 5.0f);

        Vector3 startToPosOne = posOne - m_BendingStartPos; 
        Vector3 startToPosTwo = posTwo - m_BendingStartPos;

        Vector3 crossProduct = Vector3.Cross(startToPosOne, startToPosTwo);

        float area = crossProduct.sqrMagnitude / 2;

        Debug.Log($"New Area {area}");

        m_totalArea += area; 
    }

    private void ShootAirBall()
    {


        Debug.Log($"Shooting with total area: {m_totalArea}");
        //End
        m_totalArea = 0.0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(this.transform.position, m_minNextPointDistance);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(this.transform.position, m_maxNextPointDistance);
    }
}
