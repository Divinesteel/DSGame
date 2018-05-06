using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickableObject : PlayerInteractableObject {

    protected override void OnPlayerInteract()
    {
        GetPlayerObject().GetComponent<Inventory>().AddItemToInventory(this.name);
        Destroy(this.gameObject);
    }

}
