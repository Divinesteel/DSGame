using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class RampTrigger : MonoBehaviour
{

    public GameObject rampGround;
    public GameObject weight;

    Animator animation;
    Collider weightCollider;

    //bool isRampTriggered;
    //bool isRampFalling;
    public bool isWeightRemoved;

    // Use this for initialization
    void Start()
    {
        animation = GetComponent<Animator>();
        isWeightRemoved = false;
}

    // Update is called once per frame
    void Update()
    {
        
        if (isWeightRemoved)
        {
            FallRamp();
        }

        //if (isRampTriggered)
        //{
        //    isRampFalling = animation.isPlaying;
        //    if (!isRampFalling)
        //    {
        //        rampGround.SetActive(true);
        //        isRampTriggered = false;
        //    }
        //}

    }

    public void FallRamp()
    {
        //isRampTriggered = true;
        //animation.Play("RampFall");
         animation.SetTrigger("RampTrigger");
        //isRampFalling = true; if(Input.GetKeyDown("p"))
    }

    public void OnTriggerExit(Collider weightCollider)
    {
        isWeightRemoved = true;
    }

}

