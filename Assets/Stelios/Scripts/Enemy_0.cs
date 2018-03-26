﻿using System.Collections;
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
    public bool canSee;

    Vector3 lookTowards;

    float RotateTime;
    bool hasRotated;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        destIndex = 0;
        lastKnownPosition = patrolTargetsPosition[destIndex].position;
        canSee = false;
        RotateTime = 0;
        patrolling = false;
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
                    if(agent.velocity == Vector3.zero) {
                        transform.rotation = Quaternion.Slerp(transform.rotation, patrolTargetsPosition[destIndex].rotation, RotateTime);
                        RotateTime += Time.deltaTime / RotateDuration;

                        if(Vector3.Angle(transform.forward,patrolTargetsPosition[destIndex].forward) < 0.1)
                        {
                            transform.rotation = patrolTargetsPosition[destIndex].rotation;
                        }

                    }
                    
                    if (transform.rotation == patrolTargetsPosition[destIndex].rotation)
                    {
                        hasRotated = true;
                        RotateTime = 0;
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

            
            lastKnownPosition = patrolTargetsPosition[destIndex].position;

            agent.SetDestination(target.position);

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
                hasRotated = false;
                arrived = false;
                patrolling = true;
                
            }
        }
        anim.SetFloat("Forward", agent.velocity.sqrMagnitude);
        Debug.Log(destIndex);
        Debug.Log(hasRotated);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            patrolling = false;
            canSee = true;
            target = other.gameObject.transform;
        }
    }
}
