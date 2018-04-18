using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour {

    public GameObject bridgeGround;
    public GameObject block;

    Animation animation;

    bool isBridgeTriggered;
    bool isBridgeFalling;

    public bool isLeftColumnDestroyed;
    public bool isRightColumnDestroyed;

    // Use this for initialization
    void Start () {
        animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {

        if(isLeftColumnDestroyed && isRightColumnDestroyed)
        {
            FallBridge();

            isLeftColumnDestroyed = false;
            isRightColumnDestroyed = false;
        }

        if (isBridgeTriggered)
        {
            isBridgeFalling = animation.isPlaying;
            if (!isBridgeFalling)
            {
                bridgeGround.SetActive(true);
                Destroy(block);
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
