using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    
    public List<InventoryItem> ItemList;

    public void AddItemToInventory(string name)
    {
        InventoryItem a = ScriptableObject.CreateInstance<InventoryItem>();
        a.SetItem(name);
        ItemList.Add(a);
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
