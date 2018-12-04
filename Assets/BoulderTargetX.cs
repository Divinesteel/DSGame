using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderTargetX : MonoBehaviour {

	public bool isTargetOnX;
	// Use this for initialization
	void Start () {
		isTargetOnX = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected void OnTriggerEnter(Collider other)
	{
		if (other.tag == "EnemyBody") {
			isTargetOnX = true;
		}
	}

	protected void OnTriggerExit(Collider other)
	{
		if (other.tag == "EnemyBody") {
			isTargetOnX = false;
		}
	}

	public bool GetXStatus()
	{
		return isTargetOnX;
	}
}
