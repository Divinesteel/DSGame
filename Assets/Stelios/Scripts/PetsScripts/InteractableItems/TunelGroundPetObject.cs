using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TunelGroundPetObject : MonoBehaviour {

    public Transform endTunel;
    private GroundPetInteract groundPet;
    private MoveNavGroundCompanion groundMove;
    private NavMeshAgent agent;


    // Use this for initialization  
    protected void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(agent != null)
        {
            if (agent.enabled == false)
            {
                agent.enabled = true;
            }
        }
       

        if (groundPet != null)
        {
            if (groundPet.IsDiggingStatus())
            {
                DigTunel();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GroundPet")
        {
            groundPet = other.gameObject.GetComponent<GroundPetInteract>();
            groundMove = other.gameObject.GetComponent<MoveNavGroundCompanion>();
            agent = other.gameObject.GetComponent<NavMeshAgent>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GroundPet")
        {
            groundPet = null;
        }
    }

    private void DigTunel()
    {
        agent.enabled = false;
        groundMove.StopFollowingTarget();
        Transform petTransform = groundPet.gameObject.GetComponent<Transform>();
        petTransform.position = endTunel.position;

    }

}
