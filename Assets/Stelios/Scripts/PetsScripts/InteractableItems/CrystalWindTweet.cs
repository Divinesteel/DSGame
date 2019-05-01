using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalWindTweet : TweetableObject {

    public bool tweetable;
    private Animation animation;
    public GameObject animationRoot;

    void Start()
    {
        base.Start();
        animation = GetComponent<Animation>();
    }

    protected override void OnTweetFinish()
    {
        animationRoot.GetComponent<Animation>().Play();
        GetComponents<CapsuleCollider>()[0].enabled = false;
        animation.Play();
    }

    public bool TweetableStatus()
    {
        return tweetable;
    }
}
