using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTrigger : MonoBehaviour {

	public BoulderTargetX XStatus;
	public Enemy_0 Enemy;

    private PlayerInteract playerInteract;
    private Animator boulderAnim;
	private bool isAnimPlaying;
	private float animTime;
	public bool hasAnimFinished;

	public bool isBoulderTriggered;

    // Use this for initialization
    void Start () {

        boulderAnim = GetComponent<Animator>();
		isBoulderTriggered = false;
		isAnimPlaying = false;


    }
	
	// Update is called once per frame
	void Update () {
		if (isBoulderTriggered)
        {
            Enemy.StopMoving();
            boulderAnim.SetTrigger("Boulder Trigger");
            isBoulderTriggered = false;
        }

		if (hasAnimFinished) 
		{
			Enemy.KIllThisEnemy ();
		}
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
			if (XStatus.GetXStatus()) //Checks if enemy is on X mark
			{
				playerInteract = other.gameObject.GetComponent<PlayerInteract>();
				if (playerInteract.InteractStatus())
				{
					isBoulderTriggered = true;
				}
			}
            
        }
    }

    public void ExplodeDust()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

}
