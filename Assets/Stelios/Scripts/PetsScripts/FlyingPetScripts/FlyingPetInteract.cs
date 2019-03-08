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

        

        if (Input.GetMouseButtonDown(1)) //CLICK INTERACT
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

                }
            }
        }
        
        else if (Input.GetMouseButtonUp(1))
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

        if (interactableObject != null)
        {
            try
            {
                if ((interactableObject.gameObject.GetComponent<CrystalWindTweet>().TweetableStatus() == true) && !hasFinishedTweeting)
                {

                    if (!tweetSound.isPlaying)
                    {
                        tweetSound.Play();
                    }

                    tweetVolume = 1;
                    tweetSound.volume = tweetVolume;
                    IsTweeting = true;
                }
                else if (interactableObject.gameObject.GetComponent<CrystalWindTweet>().TweetableStatus() == false)
                {
                    IsTweeting = false;
                }
            }
            catch
            {

            }
        }
        

        

        if (!IsTweeting)
        {
            if (tweetSound.volume > 0)
            {
                tweetSound.volume -= Time.deltaTime * 2;
            }
            else
            {
                if (tweetSound.isPlaying)
                {
                    tweetSound.Stop();
                }
            }
        }
    }

}
