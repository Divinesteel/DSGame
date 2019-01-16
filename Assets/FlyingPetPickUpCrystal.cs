using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPetPickUpCrystal : MonoBehaviour {

    private FlyingPetInteract flyingPetInteract;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
