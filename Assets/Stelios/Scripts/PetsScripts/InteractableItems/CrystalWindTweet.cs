using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalWindTweet : TweetableObject {

    private Animation animation;

    void Start()
    {
        base.Start();
        animation = GetComponent<Animation>();
    }

    protected override void OnTweetFinish()
    {
        animation.Play();
        GetComponents<CapsuleCollider>()[0].enabled = false;
    }
}
