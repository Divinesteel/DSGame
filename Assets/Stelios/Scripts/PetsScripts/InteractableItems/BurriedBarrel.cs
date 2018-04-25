using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurriedBarrel : Burried {

    public GameObject barel;

    private PlayerInteract playerInteract;
    private Animation barelAnim;

    private bool hasDigAnimationStarted;
    private bool hasDigAnimationPaused;

    private bool hasBeenThrownToRiver;

    // Use this for initialization
    void Start() {
        base.Start();
        barelAnim = barel.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (base.GetHasBeenDigged() == false)
        {
            if (groundPetInteract != null)
            {
                if (isPetDigging == true)
                {
                    if (!hasDigAnimationStarted)
                    {
                        barelAnim.Play();
                        hasDigAnimationStarted = true;
                    }

                    if (hasDigAnimationPaused)
                    {
                        barelAnim.enabled = true;
                        hasDigAnimationPaused = false;
                    }

                }
                else
                {
                    barelAnim.enabled = false;
                    hasDigAnimationPaused = true;
                }

            }

            if (isPetDigging && !barelAnim.IsPlaying("Animation_BurriedBarel"))
            {
                base.SetDigStatus(true);
            }
        }
        else
        {
            if(playerInteract != null)
            {
                if (!hasBeenThrownToRiver && playerInteract.isInteracting)
                {
                    barelAnim.Play("Animation_BarelRiver");
                    hasBeenThrownToRiver = true;
                }
            }
            
        }
    }

    override protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            playerInteract = other.gameObject.GetComponent<PlayerInteract>();
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if(other.gameObject.tag == "Player")
        {
            playerInteract = null;
        }
    }

}
