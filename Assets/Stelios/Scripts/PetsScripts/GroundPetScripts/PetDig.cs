using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDig : MonoBehaviour {

    public GameObject digobject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "GroundPet")
        {
            if (other.gameObject.GetComponent<GroundPetInteract>().isDigging)
            {
                digobject.SetActive(true);         
            }
        }
    }
}
