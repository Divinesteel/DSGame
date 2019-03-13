using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	private bool isInteracting;
    public KeyCode keycode;

    private PlayerController playerController;


    // Use this for initialization
    void Start () {
        playerController = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {

        if (playerController.GetPlayerStatus() == PlayerController.PlayerStat.Dead) return; //Checks whether player is Dead or Alive, so that he behaves accordingly.

        if (Input.GetKeyDown(InputManager.IM.interact))
		{
            Interact();
		}
		else if (Input.GetKeyUp(InputManager.IM.interact))
		{
            StopInteract();
		}
	}

    public void Interact()
    {
        isInteracting = true;
    }
    public void StopInteract()
    {
        isInteracting = false;
    }

    public bool InteractStatus()
    {
        return isInteracting;
    }

}
