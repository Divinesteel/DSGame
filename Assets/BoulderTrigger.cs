﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTrigger : MonoBehaviour {

    private PlayerInteract playerInteract;
    private Animator boulderAnim;

    public bool isboulderRolling;

    // Use this for initialization
    void Start () {

        boulderAnim = GetComponent<Animator>();
        isboulderRolling = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (playerInteract != null)
        {
            if (isboulderRolling)
            { 
                boulderAnim.SetTrigger("Boulder Trigger");
                isboulderRolling = false;
            }
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            playerInteract = other.gameObject.GetComponent<PlayerInteract>();
            if (playerInteract.InteractStatus())
            {
                isboulderRolling = true;
            }
        }
    }

}
