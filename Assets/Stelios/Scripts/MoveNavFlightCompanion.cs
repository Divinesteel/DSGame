using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNavFlightCompanion : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    private RaycastHit hit;
    public Transform target;
    private bool isFollowingTarget;
    public float maxDistance;
    private Animator anim;
    public float stoppingDistance;
    public Inventory inventory;
    public float PickingUpDistance;


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

        if (Mathf.Abs(CompanionPlayerVect.magnitude) < maxDistance)
        {
            if (Input.GetMouseButtonDown(1))
            {
                navMeshAgent.isStopped = false;
                isFollowingTarget = false;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                   
                        if (hit.collider.gameObject.tag == "Item")
                        {
                            Vector3 FlyingCompanionItemDistance = new Vector3((transform.position - hit.collider.gameObject.transform.position).x,
                            (transform.position - hit.collider.gameObject.transform.position).y,
                            (transform.position - hit.collider.gameObject.transform.position).z);

                            if (Mathf.Abs(FlyingCompanionItemDistance.magnitude) < PickingUpDistance)
                            {

                                hit.collider.gameObject.SetActive(false);

                                switch (hit.collider.gameObject.name)
                                {
                                    case "Key":
                                        inventory.Key = true;
                                        break;
                                }
                                return;
                            }
                        }

                        navMeshAgent.destination = hit.point;
                    
                   

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isFollowingTarget = true;
        }

        //anim.SetFloat("Walking", navMeshAgent.velocity.magnitude / navMeshAgent.speed);

    }


}
