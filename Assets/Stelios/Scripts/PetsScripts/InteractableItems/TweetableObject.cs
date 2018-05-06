using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TweetableObject : MonoBehaviour {

    private FlyingPetInteract flyingPet;

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(flyingPet != null)
        {
            if (flyingPet.HasFinishedTweetingStatus())
            {
                OnTweetFinish();
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FlyingPet")
        {
            flyingPet = other.gameObject.GetComponent<FlyingPetInteract>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FlyingPet")
        {
            flyingPet = null;
        }
    }

    abstract protected void OnTweetFinish();
    
}
