using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	public bool isInteracting;
    public KeyCode keycode;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(keycode))
		{
			isInteracting = true;
		}
		else if (Input.GetKeyUp(keycode))
		{
			isInteracting = false;
		}
	}
}
