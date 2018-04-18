using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour {

    NavMeshAgent agent;
    Animator anim;
    public Transform target;
    Vector3 lastKnownPosition;
    public Transform eye;

    bool patrolling;
    public Transform[] patrolTargets;
    private int destPoint;
    bool arrived;
    private int prevDestPoint;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        lastKnownPosition = transform.position;
	}
	
    bool CanSeeTarget()
    {
        bool canSee = false;
        Ray ray = new Ray(eye.position, target.transform.position - eye.position);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit))
        {
            if(hit.transform != target)
            {
                canSee = false;
            }
            else
            {
                lastKnownPosition = target.transform.position;
                canSee = true;
            }
        }
        return canSee;

    }
	// Update is called once per frame
	void Update () {
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
                    arrived = true;
                    StartCoroutine("GoToNextPoint");
                }
            }
            else
            {
                arrived = false;
            }
        }
        if (CanSeeTarget())
        {
            agent.SetDestination(target.transform.position);
            patrolling = false;
            if(agent.remainingDistance < agent.stoppingDistance)
            {
                anim.SetBool("Attack", true);
            }
            else
            {
                anim.SetBool("Attack", false);
            }
        }
        else
        {
            anim.SetBool("Attack", false);
            if (!patrolling)
            {
                agent.SetDestination(lastKnownPosition);
                if(agent.remainingDistance < agent.stoppingDistance)
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
        if(patrolTargets.Length == 0)
        {
            yield break;
        }
        patrolling = true;

        int timeBetweenPatrolling = Random.Range(5, 25);
        yield return new WaitForSeconds(timeBetweenPatrolling);
        arrived = false;

        destPoint = Random.Range(1, 4);
        if (destPoint == prevDestPoint)
        {
            destPoint = (destPoint + 1) % patrolTargets.Length;
        }

        agent.destination = patrolTargets[destPoint].position;

        prevDestPoint = destPoint;

        //destPoint = (destPoint + 1) % patrolTargets.Length;
    }
}
