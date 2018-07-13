using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPetInteract : Pet
{
    [Header("Dig Interaction")]
    public bool IsDigging;
    public KeyCode DiggingKey;

    private RaycastHit hit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (base.interact)
        {
            base.interact = false;
            base.instanceID = null;
        }

        if (Input.GetKeyDown(DiggingKey))
        {
            Dig();
        }
        else if (Input.GetKeyUp(DiggingKey))
        {
            StopDigging();
        }

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
                        Debug.Log(hit.collider.gameObject.GetInstanceID());
                        base.instanceID = hit.collider.gameObject.GetInstanceID();
                    }

                }
            }
        }
    }

    public void Dig()
    {
        IsDigging = true;
    }

    public void StopDigging()
    {
        IsDigging = false;
    }

    public bool IsDiggingStatus()
    {
        return IsDigging;
    }



}
