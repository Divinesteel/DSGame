using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCrystalWind : OnMouseDrop {

    public GameObject[] items;
    public string ItemNameForTrigger;

    
    protected override void OnDrop()
    {
        if(ItemNameForTrigger.Equals("Crystal Wind"))
        {
            foreach (GameObject go in items)
            {
                go.SetActive(true);
            }

        }
    }


}
