﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPetInteract : Pet
{
    private RaycastHit hit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //CLICK INTERACT
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if (hit.collider.gameObject.tag == "GroundInteractable")
                {
                    if (base.interactableObjects.Find(x => x.GetInstanceID() == hit.collider.gameObject.GetInstanceID()) != null)
                    {
                        base.interact = true;
                        base.instanceID = hit.collider.gameObject.GetInstanceID();
                    }

                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (base.interact)
            {
                base.interact = false;
                base.instanceID = null;
            }
        }
    }
}
