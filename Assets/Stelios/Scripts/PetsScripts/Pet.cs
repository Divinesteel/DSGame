using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour {

    [Header("Click Interaction")]
    public bool interact;
    public int? instanceID;
    public List<GameObject> interactableObjects;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddInteractable(GameObject go)
    {
        interactableObjects.Add(go);
    }

    public void RemoveInteractable(GameObject go)
    {
        interactableObjects.Remove(go);
    }

    public bool GetInteractStatus()
    {
        return interact;
    }

}
