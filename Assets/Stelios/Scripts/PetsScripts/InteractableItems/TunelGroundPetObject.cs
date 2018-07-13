using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunelGroundPetObject : MonoBehaviour {

    public Transform endTunel;
    private GroundPetInteract groundPet;
    private MoveNavGroundCompanion groundMove;

    // Use this for initialization  
    protected void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("Entered");
            groundPet = other.gameObject.GetComponent<GroundPetInteract>();
            groundMove = other.gameObject.GetComponent<MoveNavGroundCompanion>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GroundPet")
        {
            Debug.Log("Exited");
            groundPet = null;
        }
    }

    private void DigTunel()
    {
        Debug.Log("Trig");
        groundMove.StopFollowingTarget();
        Transform petTransform = groundPet.gameObject.GetComponent<Transform>();
        petTransform.position = endTunel.position;
    }

}
