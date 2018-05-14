using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnMouseDrop : MonoBehaviour {

    public DragAndDrop dragAndDropSystem;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate()
    {
        OnDrop();
    }

    abstract protected void OnDrop();

    protected void RemoveFromInventoryItemFromInventory(string name)
    {
        dragAndDropSystem.GetInventory().RemoveItemFromInventory(name);
        dragAndDropSystem.GetInventoryBar().RemoveItemFromBar(name);
    }

    protected DragAndDrop GetDragAndDropSystem()
    {
        return dragAndDropSystem;
    }
}
