using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPetInteract : Pet {



    [Header("Tweet Interaction")]
    public bool IsTweeting;
    public KeyCode TweetingKey;

    private AudioSource tweetSound;
    private float tweetVolume;
    private bool stopTweeting;
    private bool hasFinishedTweeting;

    private RaycastHit hit;
    private GameObject interactableObject;
    // Use this for initialization
    void Start() {
        tweetSound = GetComponent<AudioSource>();
        hasFinishedTweeting = false;
    }

    // Update is called once per frame
    void Update() {

        

        if (Input.GetKeyDown(InputManager.IM.orderFlyingPet)) //CLICK INTERACT
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "FlyingInteractable")
                {
                    interactableObject = base.interactableObjects.Find(x => x.GetInstanceID() == hit.collider.gameObject.GetInstanceID());
                    if (interactableObject != null) {
                        base.interact = true;
                        base.instanceID = hit.collider.gameObject.GetInstanceID();
                    }
                    else
                    {
                        base.distantInteractableObject = hit.collider.gameObject.name;
                    }

                }
                else
                {
                    base.distantInteractableObject = "";
                }

            }
            
        }
        
        else if (Input.GetKeyUp(InputManager.IM.orderFlyingPet))
        {
            base.interact = false;
            base.instanceID = null;
        }

        TweetController();
    }

    public bool HasFinishedTweetingStatus()
    {
        return hasFinishedTweeting;
    }


    protected void TweetController()
    {

        if (IsTweeting)
        {
            if (tweetSound.clip.length == tweetSound.time)
            {
                hasFinishedTweeting = true;
            }
        }

        if (interact)
        {
            try
            {
                if (distantInteractableObject.Equals("Crystal Wind")  && !hasFinishedTweeting)
                {

                    if (!tweetSound.isPlaying)
                    {
                        tweetSound.Play();
                    }

                    tweetVolume = 1;
                    tweetSound.volume = tweetVolume;
                    IsTweeting = true;
                }
            }
            catch
            {

            }
        }
    }

}
