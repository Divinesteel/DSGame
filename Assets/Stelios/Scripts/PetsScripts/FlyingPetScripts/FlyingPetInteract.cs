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
    // Use this for initialization
    void Start () {
        tweetSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (base.interact)
        {
            base.interact = false;
            base.instanceID = null;
        }

        HasFinishedTweetingController();

        if (Input.GetKeyDown(TweetingKey))
        {
            Tweet();
        }   
        
        else if (Input.GetKeyUp(TweetingKey))
        {
            StopTweet();
        }

        LowerTweetVolumeOnStop();

        if (Input.GetMouseButtonDown(1)) //CLICK INTERACT
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if (hit.collider.gameObject.tag == "FlyingInteractable")
                {
                    if (base.interactableObjects.Find(x => x.GetInstanceID() == hit.collider.gameObject.GetInstanceID()) != null){
                        base.interact = true;
                        base.instanceID = hit.collider.gameObject.GetInstanceID();
                    }
                    
                }
            }
        }
    }

    public void Tweet()
    {
        
        if (!tweetSound.isPlaying)
        {           
            tweetSound.Play();  
        } 

        tweetVolume = 1;
        tweetSound.volume = tweetVolume;
        IsTweeting = true;
    }

    private void LowerTweetVolumeOnStop()
    {
        if (!IsTweeting)
        {
            if (tweetSound.volume > 0)
            {
                tweetSound.volume -= Time.deltaTime;
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

    private void HasFinishedTweetingController()
    {
        hasFinishedTweeting = false;
        if (IsTweeting)
        {
            if (tweetSound.clip.length == tweetSound.time)
            {
                hasFinishedTweeting = true;
            }
        }
    }

    public bool HasFinishedTweetingStatus()
    {
        return hasFinishedTweeting;
    }

    public void StopTweet()
    {
        IsTweeting = false;
    }

}
