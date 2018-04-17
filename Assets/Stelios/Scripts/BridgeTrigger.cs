using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour {

    public GameObject block;

    Animation animation;

    bool isBridgeTriggered;
    bool isBridgeFalling;

	// Use this for initialization
	void Start () {
        animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isBridgeTriggered)
        {
            isBridgeFalling = animation.isPlaying;
            if (!isBridgeFalling)
            {
                block.SetActive(false);
                isBridgeTriggered = false;
            }           
        }
	}

    public void FallBridge()
    {
        isBridgeTriggered = true;
        animation.Play("Animation_Bridge");
        isBridgeFalling = true;
    }

}
