using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantActivation : MonoBehaviour {

    public Pet pet;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (pet.distantInteractableObject == this.gameObject.name)
        {
            pet.interact = true;
        }
    }


}
