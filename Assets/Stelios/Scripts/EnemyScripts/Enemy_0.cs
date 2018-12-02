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
            lastKnownPosition = patrolTargetsPosition[destIndex].position;

            agent.SetDestination(target.position);
            agent.speed = 2f;

            if (agent.remainingDistance < agent.stoppingDistance)
            {
                anim.SetBool("Attack", true);
                anim.SetLayerWeight(1, 1);

                Debug.Log(anim.GetCurrentAnimatorStateInfo(1).normalizedTime);
                if (anim.GetCurrentAnimatorStateInfo(1).IsName("CrossPunch") && anim.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.5)
                {
                    KillPlayer();
                    canSee = false;
                }

                
                
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
    }

	public void KIllThisEnemy()
	{
		this.gameObject.SetActive (false);
	}
}
