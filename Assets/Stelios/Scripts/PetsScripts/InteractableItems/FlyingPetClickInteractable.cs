using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPetClickInteractable : MonoBehaviour
{
    protected FlyingPetInteract pet; 
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
        if (col.gameObject.tag == "FlyingPet")
        {
            pet = col.gameObject.GetComponent<FlyingPetInteract>(); 
            Pet petscript = col.gameObject.GetComponent<Pet>();
            petscript.AddInteractable(gameObject);
        }
    }

   void OnTriggerExit(Collider col)
   {
        if (col.gameObject.tag == "FlyingPet")
        {
            pet = null;
            Pet petscript = col.gameObject.GetComponent<Pet>();
            petscript.RemoveInteractable(gameObject);
        }
   }
}
