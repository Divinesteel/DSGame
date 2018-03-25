using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_0 : MonoBehaviour
{

    NavMeshAgent agent;
    Animator anim;
    Vector3 lastKnownPosition;

    bool patrolling;

    private Transform target;

    public Transform[] patrolTargetsPosition;
    public float[] patrolTargetsTime;
    public float RotateDuration;

    private int destIndex;

    bool arrived;
    private int prevDestPoint;
    bool canSee;

    Vector3 lookTowards;

    float RotateTime;
    bool hasRotated;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        lastKnownPosition = transform.position;
        canSee = false;
        destIndex = 0;
        RotateTime = 0;
        patrolling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.pathPending)
        {
            return;
        }

        if (patrolling)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                if (!arrived)
                {

                    transform.rotation = Quaternion.Slerp(transform.rotation, patrolTargetsPosition[destIndex].rotation, RotateTime);
                    RotateTime += Time.deltaTime / RotateDuration;
                    if (RotateTime >= 1)
                    {
                        hasRotated = true;
                    }
                    

                    if (hasRotated)
                    {
                        arrived = true;
                        destIndex = (destIndex + 1) % patrolTargetsPosition.Length;
                        StartCoroutine("GoToNextPoint");
                    }

                }
            }
            else
            {
                arrived = false;
            }
        }

        if (canSee)
        {
            agent.SetDestination(target.transform.position);
            patrolling = false;
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                //anim.SetBool("Attack", true);
            }
            else
            {
                //anim.SetBool("Attack", false);
            }
        }
        else
        {
            //anim.SetBool("Attack", false);
            if (!patrolling)
            {
                agent.SetDestination(lastKnownPosition);
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    patrolling = true;
                    StartCoroutine("GoToNextPoint");
                }
            }
        }
        anim.SetFloat("Forward", agent.velocity.sqrMagnitude);
    }

    IEnumerator GoToNextPoint()
    {
        if (patrolTargetsPosition.Length == 0)
        {
            yield break;
        }

        patrolling = true;

        yield return new WaitForSeconds(patrolTargetsTime[destIndex]);

        arrived = false;

        agent.destination = patrolTargetsPosition[destIndex].position;
        hasRotated = false;
    }


    void onTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            canSee = true;
            target = other.gameObject.transform;
        }
    }
}
