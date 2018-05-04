using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInteractableObject : MonoBehaviour {

    private PlayerInteract playerInteract;

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
        if(playerInteract != null)
        {
            if (playerInteract.InteractStatus())
            {
                OnPlayerInteract();
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInteract = other.gameObject.GetComponent<PlayerInteract>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInteract = null;
        }
    }

    abstract protected void OnPlayerInteract();

    protected GameObject GetPlayerObject()
    {
        return playerInteract.gameObject;
    }

}
