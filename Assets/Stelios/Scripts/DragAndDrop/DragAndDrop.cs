using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {
    
    public Inventory inventoryPlayer;
    public InventoryUIBar inventoryBar;

    private string draggedName;

    private Vector3 mousePos;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDraggedItem(string name)
    {
        draggedName = name;
    }

    public void RemoveDraggedItem()
    {
        //RemoveFromInventoryOnDrop(draggedName);
        draggedName = null;   
    }
    
    public void DropItem()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.gameObject.tag == "MouseDrop")
            {
                OnMouseDrop onMouseDrop = hit.collider.gameObject.GetComponent<OnMouseDrop>();
                onMouseDrop.Activate();
            }
        }
    }

    public Inventory GetInventory()
    {
        return inventoryPlayer;
    }

    public InventoryUIBar GetInventoryBar()
    {
        return inventoryBar;
    }

    public string GetDraggedName()
    {
        return draggedName;
    }
}
