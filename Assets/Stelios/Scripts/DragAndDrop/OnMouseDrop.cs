using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnMouseDrop : MonoBehaviour {

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate()
    {
        OnDrop();
    }

    abstract protected void OnDrop();
}
