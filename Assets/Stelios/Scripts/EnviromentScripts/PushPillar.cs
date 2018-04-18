using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPillar : MonoBehaviour {

	public GameObject Player;
	private Animation animation;

	// Use this for initialization
	void Start () {

		animation = GetComponent<Animation>();
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				animation.Play();
			}
		}
	}

}
