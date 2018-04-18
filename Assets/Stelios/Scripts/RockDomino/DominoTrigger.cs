﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoTrigger : FlyingPetClickInteractable
{
    private Rigidbody[] rbs;
    private Animation animation;
    private bool hasBeenTriggered;
    private bool hasBeenDestroyed;

    public GameObject BridgeRightColumn;
    private Rigidbody[] bridgerbs;

    // Use this for initialization
    void Start()
    {
        rbs = transform.parent.gameObject.GetComponentsInChildren<Rigidbody>();
        animation = transform.parent.transform.parent.GetComponent<Animation>();
        bridgerbs = BridgeRightColumn.GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(base.pet != null && !hasBeenTriggered)
        {
            if (base.pet.interact == true)
            {
                animation.Play("RockTrigger");
                hasBeenTriggered = true;
            }
        }

        
    }

    void FixedUpdate()
    {
        if (hasBeenTriggered && !hasBeenDestroyed)
        {
            if (!animation.IsPlaying("RockTrigger"))
            {
                foreach (Rigidbody rb in rbs)
                {
                    rb.isKinematic = false;
                    rb.AddForce(new Vector3(Random.Range(-40, 40), Random.Range(-40, 40), Random.Range(-40, 40)));
                }

                foreach (Rigidbody rb in bridgerbs)
                {
                    rb.isKinematic = false;
                    rb.AddForce(new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20)));
                    rb.AddTorque(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));

                }

                hasBeenTriggered = true;
            }
        }
    }
}



