using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem : ScriptableObject {

    public string itemName;
	public Sprite image;

	public void SetImage(Sprite img)
	{
		image = img;
	}

	public Sprite getImage()
	{
		return image;
	}

    public void SetName(string name)
    {
        itemName = name;
    }

    public string getName()
    {
        return itemName;
    }

}
