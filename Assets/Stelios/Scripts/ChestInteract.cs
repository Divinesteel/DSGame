using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : MonoBehaviour {

    Animation anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        anim.Play("Chest_Dig");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
