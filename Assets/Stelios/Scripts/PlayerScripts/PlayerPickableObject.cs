using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickableObject : PlayerInteractableObject {


    protected override void OnPlayerInteract()
    {
		GetPlayerObject().GetComponent<Inventory>().AddItemToInventory(GetComponent<CrystalObject>().name,GetComponent<CrystalObject>().image);
        Destroy(this.gameObject);

        CheckpointCtrl.CPC.SaveCheckpoint();
    }

}
