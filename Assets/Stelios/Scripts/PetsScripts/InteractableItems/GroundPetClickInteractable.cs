using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPetClickInteractable : MonoBehaviour
{
    public GroundPetInteract pet;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "GroundPet")
        {
            pet = col.gameObject.GetComponent<GroundPetInteract>();
            Pet petscript = col.gameObject.GetComponent<Pet>();
            petscript.AddInteractable(gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "GroundPet")
        {
            pet = null;
            Pet petscript = col.gameObject.GetComponent<Pet>();
            petscript.RemoveInteractable(gameObject);
        }
    }

}
