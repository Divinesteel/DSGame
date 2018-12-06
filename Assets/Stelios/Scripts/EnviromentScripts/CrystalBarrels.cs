using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBarrels : GroundPetClickInteractable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (base.pet != null)
        {
            if (base.pet.interact == true)
            {
                Debug.Log("TRIG");
            }
        }

    }
}
