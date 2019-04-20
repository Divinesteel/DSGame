using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurriedBarrel : Burried {

    public GameObject barel;

    private PlayerInteract playerInteract;
    private Animation barelAnim;

    public GameObject TextPrompt;
    public Text TextValue;

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
                }
            }

            if (hasDigAnimationStarted && !barelAnim.IsPlaying("Animation_BurriedBarel"))
            {
                base.SetDigStatusTrue();
            }
        }
        else
        {
            if(playerInteract != null)
            {
                if (!hasBeenThrownToRiver && playerInteract.InteractStatus())
                {
                    barelAnim.Play("Animation_BarelRiver");
                    hasBeenThrownToRiver = true;
                    foreach (CapsuleCollider cc in GetComponents<CapsuleCollider>())
                    {
                        TextPrompt.SetActive(false);
                        TextValue.text = "Pick Up";
                        cc.enabled = false;
                    }
                }
            }
            
        }
    }

    override protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            if (base.hasBeingDigged)
            {
                TextValue.text = "Throw";
                TextPrompt.SetActive(true);
                TextPrompt.transform.position = transform.GetChild(0).position + new Vector3(0, 2, 0);
            }
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
