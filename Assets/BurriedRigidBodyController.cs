using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BurriedRigidBodyController : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<NavMeshObstacle>().enabled = true;
    }

    // Update is called once per frame
    void Update () {
        if (GetComponentInParent<Burried>().hasBeingDigged == true )
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<BoxCollider>().enabled = true;
            //GetComponent<NavMeshAgent>().enabled = true;
            //GetComponent<NavMeshObstacle>().enabled = false;
            GetComponentInParent<Burried>().hasBeingDigged = false;
            GetComponentInParent<Burried>().enabled = false;
        }
	}
}
