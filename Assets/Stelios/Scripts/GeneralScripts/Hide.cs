using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour {

    private Color playercolor;

	// Use this for initialization
	void Start () {
        playercolor =  GameObject.Find("Peasant_Man").GetComponent<SkinnedMeshRenderer>().material.color;

        //Debug.Log(playercolor);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //playercolor.a = 0.5f;
            //Debug.Log(playercolor);
            GameObject.Find("Peasant_Man").GetComponent<SkinnedMeshRenderer>().material.color = new Color (0,0,0,0.5f);
            other.gameObject.tag = "Player_Hidden";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player_Hidden")
        {
            //playercolor.a = 1;
            //Debug.Log(playercolor);
            GameObject.Find("Peasant_Man").GetComponent<SkinnedMeshRenderer>().material.color = playercolor;
            other.gameObject.tag = "Player";
        }
    }
}
