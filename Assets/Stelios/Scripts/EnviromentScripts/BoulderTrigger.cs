using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoulderTrigger : MonoBehaviour {

    public GameObject player;
    public BoulderTargetX XStatus;
	public Enemy_0 Enemy;

    public GameObject TextPrompt;
    public Text TextValue;

    private PlayerInteract playerInteract;
    private Animator boulderAnim;
	private bool isAnimPlaying;
	private float animTime;
	public bool hasAnimFinished;

	public bool isBoulderTriggered;

    // Use this for initialization
    void Start () {

        //TextPrompt = GameObject.Find("Interact Promt CANVAS");
        //TextValue = GameObject.Find("Interaction Text").GetComponent<Text>();

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
			Enemy.KIllThisEnemy();
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
			if (XStatus.GetXStatus()) //Checks if enemy is on X mark
			{
                TextValue.text = "Push";
                TextPrompt.SetActive(true);
                TextPrompt.transform.position = transform.position + new Vector3(0, 2, 0);

                playerInteract = other.gameObject.GetComponent<PlayerInteract>();
				if (playerInteract.InteractStatus())
				{
					isBoulderTriggered = true;
                    player.GetComponent<CheckpointCtrl>().SaveCheckpoint();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            TextPrompt.SetActive(false);
            TextValue.text = "Pick Up";
        }
    }
    public void ExplodeDust()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

}
