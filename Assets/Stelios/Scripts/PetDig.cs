using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDig : MonoBehaviour {

    public GameObject digobject;
    private Animation anim;

	// Use this for initialization
	void Start () {
        anim = digobject.GetComponent<Animation>();
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
                digobject.SetActive(true);         
            }
        }
    }
}
