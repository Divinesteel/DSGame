using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDig : MonoBehaviour {

    public GameObject chest;
    private Animation anim;

	// Use this for initialization
	void Start () {
        anim = chest.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "GroundPet")
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                chest.SetActive(true);
                anim.Play("Chest_Dig");
                
            }
        }
    }
}
