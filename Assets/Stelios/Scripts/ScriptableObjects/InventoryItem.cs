using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem : ScriptableObject {

    public string itemName;

    public void SetItem(string name)
    {
        itemName = name;
    }

    public string getName()
    {
        return itemName;
    }

}
