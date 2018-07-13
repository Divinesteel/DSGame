﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNavGroundCompanion : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    private RaycastHit hit;
    public Transform target;
    private bool isFollowingTarget;
    public float maxDistance;
    private Animator anim;
    public float stoppingDistance;

    // Use this for initialization
    void Start()
    {
        isFollowingTarget = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowingTarget)
        {
            
            navMeshAgent.destination = target.transform.position;
            anim.SetFloat("Walking", navMeshAgent.velocity.sqrMagnitude);
            

            if (navMeshAgent.remainingDistance < stoppingDistance)
            {
                navMeshAgent.isStopped = true;               
            }
            else
            {
                navMeshAgent.isStopped = false;
            }
        }

        //Ray CompanionPlayerRay = new Ray(transform.position, target.transform.position - transform.position);
        Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
        Vector3 CompanionPlayerVect = new Vector3((transform.position - target.transform.position).x,
            (transform.position - target.transform.position).y,
            (transform.position - target.transform.position).z);

        if (Mathf.Abs(CompanionPlayerVect.magnitude) < maxDistance) // check if pet is within command range
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //create ray from camera to mouse position
                Debug.DrawRay(hit.point, target.transform.position - hit.point, Color.blue); //create ray from player to hit point
                if (Physics.Raycast(ray.origin, ray.direction, out hit) && (target.transform.position - hit.point).magnitude < maxDistance)
                {
                    if (hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 12) // 9 = Ground, 12 = Wind
                    {
                        navMeshAgent.isStopped = false;
                        isFollowingTarget = false;
                        navMeshAgent.destination = hit.point;
                    }
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isFollowingTarget = true;
        }

        anim.SetFloat("Walking", navMeshAgent.velocity.sqrMagnitude);
    }

    public void StopFollowingTarget()
    {
        isFollowingTarget = false;
        navMeshAgent.isStopped = true;
    }
}
