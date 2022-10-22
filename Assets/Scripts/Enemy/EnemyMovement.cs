using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] bool isWalking = true;

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool isTriggered = false; 
    public bool IsTriggered
    {
        get { return isTriggered; }
        set { isTriggered = value; }
    }


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        destPoint = Random.Range(0, points.Length);
        GoToNextPoint();
    }


    void GoToNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = Random.Range(0, points.Length);
    }


    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 2f && !isTriggered && isWalking)
            GoToNextPoint();
    }
}
