using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbable : MonoBehaviour {

    public Transform endPosition;
    public float height;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            other.gameObject.GetComponent<PlayerMovement>().SetJumpDestination(endPosition,height);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            other.gameObject.GetComponent<PlayerMovement>().SetJumpDestination(null,0);
        }
    }
}
