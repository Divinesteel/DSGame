using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : MonoBehaviour {

    Animation anim;
    private bool isChestClosed;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        anim.Play("Chest_Dig");

        isChestClosed = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerInteract>().isInteracting && isChestClosed)
            {
                if (other.gameObject.GetComponent<Inventory>().Key)
                {
                    isChestClosed = false;
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
