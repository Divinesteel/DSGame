using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TweetableObject : MonoBehaviour {

    private FlyingPetInteract flyingPet;
    private bool hasBeenTriggered;

	// Use this for initialization
	protected void Start () {
        hasBeenTriggered = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(flyingPet != null)
        {
            if (flyingPet.HasFinishedTweetingStatus() && !hasBeenTriggered)
            {
                OnTweetFinish();
                hasBeenTriggered = true;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FlyingPet")
        {
            flyingPet = other.gameObject.GetComponent<FlyingPetInteract>();
            Pet petscript = other.gameObject.GetComponent<Pet>();
            petscript.AddInteractable(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FlyingPet")
        {
            flyingPet = null;
            Pet petscript = other.gameObject.GetComponent<Pet>();
            petscript.RemoveInteractable(gameObject);
        }
    }

    abstract protected void OnTweetFinish();
    
}
