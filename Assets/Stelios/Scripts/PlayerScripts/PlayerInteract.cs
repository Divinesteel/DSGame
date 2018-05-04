using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	private bool isInteracting;
    public KeyCode keycode;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(keycode))
		{
            Interact();
		}
		else if (Input.GetKeyUp(keycode))
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
