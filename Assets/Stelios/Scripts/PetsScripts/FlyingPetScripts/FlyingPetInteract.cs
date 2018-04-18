using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPetInteract : Pet {

    

    [Header("Tweet Interaction")]
    public bool IsTweeting;
    public KeyCode TweetingKey;

    private RaycastHit hit;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (base.interact)
        {
            base.interact = false;
            base.instanceID = null;
        }

        if (Input.GetKeyDown(TweetingKey))
        {
            IsTweeting = true;
        }
        else if (Input.GetKeyUp(TweetingKey))
        {
            IsTweeting = false;
        }

        if (Input.GetMouseButtonDown(1)) //CLICK INTERACT
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if (hit.collider.gameObject.tag == "FlyingInteractable")
                {
                    if (interactableObjects.Find(x => x.GetInstanceID() == hit.collider.gameObject.GetInstanceID()) != null){
                        base.interact = true;
                        Debug.Log(hit.collider.gameObject.GetInstanceID());
                        base.instanceID = hit.collider.gameObject.GetInstanceID();
                    }
                    
                }
            }
        }
    }

    public void Sing()
    {
        IsTweeting = true;
    }

    public void StopSinging()
    {
        IsTweeting = false;
    }



}
