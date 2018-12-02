using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public InventoryUIBar inventoryBar;
    
    public List<InventoryItem> ItemList;

	public void AddItemToInventory(string name, Sprite img)
    {
        InventoryItem a = ScriptableObject.CreateInstance<InventoryItem>();
        a.SetName(name);
		a.SetImage(img);
        ItemList.Add(a);

        inventoryBar.AddItemToBar(name,img);
    }

    public void RemoveItemFromInventory(string name)
    {
        ItemList.Remove(ItemList.Find(x => x.getName() == name));
    }

    public InventoryItem FindItemOnInventory(string name)
    {
        return ItemList.Find(x => x.getName() == name);
    }
	
}
