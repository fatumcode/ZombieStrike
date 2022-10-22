using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    Transform target;
    NavMeshAgent navMeshAgent;
    float distanseToTarget = Mathf.Infinity;
    bool isProvoked = false;
    private int delayCount = 0;
    private int maxDelay = 10;
    private bool wasLastVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanseToTarget = Vector3.Distance(target.position, transform.position);
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
        var isVisible = Physics.Raycast(transform.position, Vector3.ClampMagnitude(transform.forward, chaseRange), chaseRange);
        if (isProvoked)
        {
            EngageTarget();
        }
         else if(distanseToTarget <= chaseRange && IsPlayerVisible())
        {
            isProvoked = true;
            GetComponent<EnemyMovement>().IsTriggered = true;
        }
    }

    bool IsPlayerVisible()
    {
        delayCount++;
        if (delayCount == maxDelay)
        {
            //reset the count
            delayCount = 0;

            Vector3 direction = target.position - transform.position;
            //save the first hit into a variable.
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.transform.tag.Equals("Player"))
                     wasLastVisible = true;
                else
                    wasLastVisible = false;
            }
        }
        return wasLastVisible;

    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanseToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanseToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }


    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
        turnSpeed = 1;
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
        turnSpeed = 5;
    }


    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, Time.deltaTime * turnSpeed);
    }
   

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }


}
