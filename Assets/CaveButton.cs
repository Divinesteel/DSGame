using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveButton : MonoBehaviour {

    public bool isButtonPressed;
    Animation buttonAnimation;

	// Use this for initialization
	void Start () {
        buttonAnimation = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "GroundPet")
        {
            buttonAnimation["CaveButtonPress"].speed = 1;
            buttonAnimation.Play("CaveButtonPress");
        }
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
            buttonAnimation["CaveButtonPress"].speed = -1;
            buttonAnimation["CaveButtonPress"].time = buttonAnimation["CaveButtonPress"].length;
            buttonAnimation.Play("CaveButtonPress");


            isButtonPressed = false;
        }
    }

    public bool GetButtonStatus()
    {
        return isButtonPressed;
    }
}
