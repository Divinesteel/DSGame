using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFlyingWay : MonoBehaviour {
	
	private RaycastHit hit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)) 
		{
			if (hit.collider.gameObject == this.gameObject) {
				GetComponentInChildren<ParticleSystem> ().Play ();
			} 
			else 
			{
				GetComponentInChildren<ParticleSystem> ().Clear ();
				GetComponentInChildren<ParticleSystem> ().Stop ();
			}
		}
	}
}
