using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPetPickUpCrystal : MonoBehaviour {

    private FlyingPetInteract flyingPetInteract;
    private bool hasBeenTriggered;

	// Use this for initialization
	void Start () {
        hasBeenTriggered = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (flyingPetInteract != null)
        {
            if (flyingPetInteract.GetInteractStatus() && !hasBeenTriggered)
            {
                hasBeenTriggered = true;
            }
        }

        if (hasBeenTriggered)
        {
            transform.position = new Vector3(flyingPetInteract.gameObject.transform.position.x, flyingPetInteract.gameObject.transform.position.y + (float)0.9, flyingPetInteract.gameObject.transform.position.z);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FlyingPet")
        {
            flyingPetInteract = other.gameObject.GetComponent<FlyingPetInteract>();
            Pet petscript = other.gameObject.GetComponent<Pet>();
            petscript.AddInteractable(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FlyingPet")
        {
            flyingPetInteract = null;
            Pet petscript = other.gameObject.GetComponent<Pet>();
            petscript.RemoveInteractable(gameObject);
        }
    }
}
