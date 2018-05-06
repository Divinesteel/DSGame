using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasItemToInteract : MonoBehaviour {

    public Inventory inventory;
    Animation anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.gameObject.tag == "Player")
            {
                if (inventory.FindItemOnInventory("Key"))
                {
                    anim.Play("Open_Chest");
                }
                else
                {
                    anim.Play("Locked_Chest");
                }
            }

        }
             
    }
}
