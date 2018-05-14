using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCrystal : OnMouseDrop {

    public GameObject[] items;
    public string ItemNameForTrigger;
    

    protected override void OnDrop()
    {
        if (ItemNameForTrigger.Equals(GetDragAndDropSystem().GetDraggedName()))
        {
            foreach (GameObject go in items)
            {
                go.SetActive(true);
            }
            base.RemoveFromInventoryItemFromInventory(GetDragAndDropSystem().GetDraggedName());

        }
    }

    

}
