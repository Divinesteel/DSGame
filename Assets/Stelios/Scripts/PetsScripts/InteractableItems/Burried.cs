using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burried : MonoBehaviour {

    private bool hasBeingDigged;
    
    public bool isPetDigging;
    public GroundPetInteract groundPetInteract;

    [Header("Dust Particle System")]
    public ParticleSystem DustParticleSystem;
	// Use this for initialization
	protected void Start () {

	}

    // Update is called once per frame
    protected void Update () {

        if(groundPetInteract != null)
        {
            isPetDigging = groundPetInteract.GetInteractStatus();
            if(isPetDigging == true && !hasBeingDigged)
            {
                DustParticleSystem.Play();
            }
            else
            {
                DustParticleSystem.Stop();
            }
        }

	}

    protected void SetDigStatus(bool a)
    {
        hasBeingDigged = a;
    }

    protected bool GetHasBeenDigged()
    {
        return hasBeingDigged;
    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GroundPet")
        {
            groundPetInteract = other.GetComponent<GroundPetInteract>();
            Pet petscript = other.gameObject.GetComponent<Pet>();
            petscript.AddInteractable(gameObject);
        }
    }

    virtual protected void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "GroundPet")
        {
            groundPetInteract = null;
            Pet petscript = other.gameObject.GetComponent<Pet>();
            petscript.RemoveInteractable(gameObject);
        }
    }
}
