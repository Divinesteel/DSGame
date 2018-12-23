using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveButton : MonoBehaviour {

    public bool isButtonPressed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "GroundPet")
        {
            isButtonPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "GroundPet")
        {
            isButtonPressed = false;
        }
    }

    public bool GetButtonStatus()
    {
        return isButtonPressed;
    }
}
