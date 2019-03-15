using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNavFlightCompanion : MonoBehaviour
{ 
    public Transform target;  
    public float maxDistance;
    public float stoppingDistance;

    private Animator anim;
    private NavMeshPath pathToTarget;
    public bool isFollowingTarget;
    private NavMeshAgent navMeshAgent;
    private RaycastHit hit;

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
            pathToTarget = new NavMeshPath();
            navMeshAgent.CalculatePath(target.transform.position, pathToTarget); //Checks if there is available path to Player

            if (pathToTarget.status == NavMeshPathStatus.PathInvalid || pathToTarget.status == NavMeshPathStatus.PathPartial)
            {

            }
            else
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
        }

        //Ray CompanionPlayerRay = new Ray(transform.position, target.transform.position - transform.position);
        Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
        Vector3 CompanionPlayerVect = new Vector3((transform.position - target.transform.position).x,
            (transform.position - target.transform.position).y,
            (transform.position - target.transform.position).z);

        if (Mathf.Abs(CompanionPlayerVect.magnitude) < maxDistance)
        {
            if (!PauseMenu.gameIsPaused && Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //create ray from camera to mouse position
                Debug.DrawRay(hit.point, target.transform.position - hit.point, Color.blue); //create ray from player to hit point
                if (Physics.Raycast(ray.origin, ray.direction, out hit) && (target.transform.position - hit.point).magnitude < maxDistance)
                {
                    if (hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 10) // 9 = Ground, 10 = Flying
                    {                     
                        isFollowingTarget = false;
                                          
                        pathToTarget = new NavMeshPath();
                        navMeshAgent.CalculatePath(hit.point, pathToTarget); //Checks if there is Available Path to Destination
                        if (pathToTarget.status == NavMeshPathStatus.PathInvalid || pathToTarget.status == NavMeshPathStatus.PathPartial)
                        {

                        }
                        else
                        {
                            navMeshAgent.destination = hit.point;
                            navMeshAgent.isStopped = false;
                        }
                    }    
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
