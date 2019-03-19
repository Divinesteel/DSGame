﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy_0 : MonoBehaviour
{
    public PlayerController playerController;

    NavMeshAgent agent;
    Animator anim;
    Vector3 lastKnownPosition;
    [SerializeField] bool patrolling;

    private Transform target;

    public Transform[] patrolTargetsPosition;
    public float[] patrolTargetsTime;
    public float RotateDuration;
    [SerializeField] private int destIndex;
    private bool stopMoving;

    bool arrived;
    private int prevDestPoint;
    public bool canSee;
    private float startingSpeed;

    Vector3 lookTowards;

    float RotateTime;
    bool hasRotated;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startingSpeed = agent.speed;
        anim = GetComponent<Animator>();
        destIndex = 0;
        lastKnownPosition = patrolTargetsPosition[destIndex].position;
        canSee = false;
        RotateTime = 0;
        patrolling = false;
        stopMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.pathPending || stopMoving)
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

                        gameObject.RotateAnimation(anim, patrolTargetsPosition[destIndex].rotation); //Sets the object's Animator to rotate either left or right.
                                           
                        RotateTime += (Time.deltaTime * Time.deltaTime) / RotateDuration;

                        if(Vector3.Angle(transform.forward,patrolTargetsPosition[destIndex].forward) < 1)
                        {
                            anim.StopRotate(); //Stops Animation Rotation.
                            transform.rotation = patrolTargetsPosition[destIndex].rotation;
                            hasRotated = true;
                            RotateTime = 0;
                        }
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
            anim.StopRotate(); //Stops Animation Rotation.
            //lastKnownPosition = patrolTargetsPosition[destIndex].position;

            agent.speed = 2f;

            //NavMeshPath path = new NavMeshPath();
            //agent.CalculatePath(target.position, path);
            double x = Math.Pow(transform.position.x - target.transform.position.x, 2);
            double y = Math.Pow(transform.position.y - target.transform.position.y, 2);
            double z = Math.Pow(transform.position.z - target.transform.position.z, 2);
            double distance = Math.Sqrt(x + y + z);

            agent.destination = target.position;

            if (distance <= agent.stoppingDistance + 1)
            {
                //Debug.Log(anim.GetCurrentAnimatorStateInfo(1).normalizedTime);
                anim.SetBool("Attack", true);
                anim.SetLayerWeight(1, 1);
                if (anim.GetCurrentAnimatorStateInfo(1).IsName("CrossPunch") && anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.60)
                {
                    GetComponent<AudioSource>().Play();
                    KillPlayer();
                    canSee = false;
                }
            }
        }
        else
        {
            if (!patrolling)
            {
                //agent.SetDestination(lastKnownPosition);
                hasRotated = false;
                arrived = false;
                patrolling = true;
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            patrolling = false;
            canSee = true;
            target = other.gameObject.transform;
        }
    }

    void KillPlayer()
    {
        playerController.KillPlayer();
        agent.ResetPath();
    }

	public void KIllThisEnemy()
	{
		this.gameObject.SetActive (false);
	}

    public void StopMoving()
    {
        stopMoving = true;
    }

    public void ResetState()
    {
            transform.position = patrolTargetsPosition[0].position;
            transform.rotation = patrolTargetsPosition[0].rotation;

            destIndex = 0;
            canSee = false;
            RotateTime = 0;
            patrolling = false;
            stopMoving = false;
            hasRotated = false;
            arrived = true;

            agent.speed = startingSpeed;
            anim.SetBool("Attack", false);
            anim.SetLayerWeight(1, 0);
            agent.ResetPath();
    }
}
