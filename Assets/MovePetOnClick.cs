using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePetOnClick : MonoBehaviour {

    public GameObject pet;
    NavMeshAgent agent;
    public GameObject moveLocation;

    private int? mousePetCode; 

	// Use this for initialization
	void Start () {
        agent = pet.GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (mousePetCode != null)
        {
            if (Input.GetMouseButton((int) mousePetCode))
            {
                agent.destination = moveLocation.transform.position;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (pet.tag == "FlyingPet")
        {
            mousePetCode = 1;
        }
        else if (pet.tag == "GroundPet")
        {
            mousePetCode = 0;
        }
    }

    private void OnMouseExit()
    {
        mousePetCode = null;
    }



}
