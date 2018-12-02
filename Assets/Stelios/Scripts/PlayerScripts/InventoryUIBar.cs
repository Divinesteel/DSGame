using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIBar : MonoBehaviour {

    public GameObject invButtons;
	public Sprite defaultSlot;
    public Text[] invItemText;

	// Use this for initialization
	void Start () {
        invItemText = invButtons.GetComponentsInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddItemToBar(string itemName, Sprite itemImage)
    {
        int i = 0;
        while (!invItemText[i].text.Equals(""))
        {
            i += 1;
        }

		invItemText[i].text = itemName;
		invItemText [i].GetComponentInParent<Image> ().sprite = itemImage;


    }

    public void RemoveItemFromBar(string name)
    {
        bool removed = false;
        int i = 0;
        while (!removed)
        {
            if (invItemText[i].text.Equals(name))
            {
                invItemText[i].text = null;
				invItemText[i].GetComponentInParent<Image>().sprite = defaultSlot;
                removed = true;
            }
            i += 1;
        }
    }
}
