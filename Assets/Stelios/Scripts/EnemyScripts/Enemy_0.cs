using System;
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

    Vector3 tempPos;
    Quaternion tempRot;
    int tempDest;
    NavMeshPath tempPath;

    bool spotted;

    bool temphasRotated ;
    bool tempArrived;
    bool tempCanSee;
    bool tempPatrolling;

    private Transform target;

    public Transform[] patrolTargetsPosition;
    public float[] patrolTargetsTime;
    public float RotateDuration;
    [SerializeField] private int destIndex;
    private bool stopMoving;

    private int prevDestPoint;
    private float startingSpeed;

    Vector3 lookTowards;

    float RotateTime;

    [SerializeField] bool hasRotated;
    [SerializeField] bool patrolling;
    [SerializeField] bool arrived;
    [SerializeField] bool canSee;
    [SerializeField] bool alerted;
    [SerializeField] bool tigerGrowling;

    public AudioSource PunchClip;
    public AudioSource AlertClip;

    // Use this for initialization
    void Start()
    {
        alerted = false;
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
            transform.Find("Spotlight").GetComponent<Light>().color = new Color(255, 255, 255, 255);
            transform.Find("Spotlight").GetComponent<Light>().intensity = 0.05f;

            if (agent.enabled && agent.remainingDistance < agent.stoppingDistance)
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

        if (agent.enabled && canSee)
        {
            if (!alerted)
            {
                AlertClip.Play();
                alerted = true;
            }
            transform.Find("Spotlight").GetComponent<Light>().color = new Color(255, 0, 0, 255);
            transform.Find("Spotlight").GetComponent<Light>().intensity = 0.1f;

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
                    PunchClip.Play();
                    KillPlayer();
                    canSee = false;
                    alerted = false;
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
        if( agent.enabled)
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
    }

    void OnTriggerStay(Collider other)
    {
        #region Chase Player

        if (other.gameObject.tag == "Player")
        {
            spotted = true;
            agent.enabled = true;
            //agent.SetDestination(lastKnownPosition);
            //agent.path = tempPath;
            //destIndex = tempDest;
            patrolling = false;
            canSee = true;
            target = other.gameObject.transform;
            tigerGrowling = false;
        }

        #endregion

        #region Freeze-Scared by Tiger

        if (other.gameObject.tag == "GroundPet" && !spotted)
        {
            if (Input.GetKey(InputManager.IM.orderGroundPet))
            {
                transform.Find("Spotlight").GetComponent<Light>().color = new Color(0, 80, 255, 255);
                if (!tigerGrowling)
                {
                    other.gameObject.GetComponent<AudioSource>().Play();
                    tigerGrowling = true;
                }
                tempDest = destIndex;
                tempPath = agent.path;
                //agent.ResetPath();
                patrolling = false;
                agent.enabled = false;
            }
            else
            {
                other.gameObject.GetComponent<AudioSource>().Stop();
                tigerGrowling = false;
                patrolling = true;
                agent.enabled = true;
                agent.SetDestination(lastKnownPosition);
                agent.path = tempPath;
                destIndex = tempDest;
            }
        }
        #endregion
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "GroundPet")
    //    {
    //        other.gameObject.GetComponent<AudioSource>().Stop();
    //        tigerGrowling = false;
    //        patrolling = true;
    //        agent.enabled = true;
    //        agent.SetDestination(lastKnownPosition);
    //        agent.path = tempPath;
    //        destIndex = tempDest;
    //    }
    //}

    void KillPlayer()
    {
        playerController.KillPlayer();
        spotted = false;
        //agent.ResetPath();
    }

	public void KIllThisEnemy()
	{
        //this.gameObject.SetActive(false);
        this.agent.enabled = false;
        transform.position = new Vector3 (0, -34, 0);
        //Destroy(this.gameObject);
    }

    public void StopMoving()
    {
        stopMoving = true;
    }

    public void SaveState()
    {
        temphasRotated = hasRotated ;
        tempArrived = arrived;
        tempCanSee = canSee;
        tempPatrolling = patrolling;

        tempPos = transform.position;
        tempRot = transform.rotation;
        tempDest = destIndex;
        tempPath = agent.path;
    }

    public void ResetState()
    {
        //agent.ResetPath();
        //KIllThisEnemy();

        transform.position = tempPos;
        transform.rotation = tempRot;

        agent.speed = startingSpeed;
        anim.SetBool("Attack", false);
        anim.SetLayerWeight(1, 0);

        destIndex = tempDest;

        agent.path = tempPath;
        //if (patrolTargetsPosition.Length > 1)
        //{
        agent.destination = patrolTargetsPosition[destIndex].position;
        //}

        hasRotated = temphasRotated;
        arrived = tempArrived;
        canSee = tempCanSee;
        patrolling = tempPatrolling;

        //RotateTime = 0;
        //stopMoving = false;
    }
}
