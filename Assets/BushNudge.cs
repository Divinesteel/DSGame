using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushNudge : MonoBehaviour {

    public Quaternion rot;
    public int frames = 0;

    // Use this for initialization
    void Start () {

        rot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.tag = "Player_Hidden";
        }
        if (other.gameObject.tag == "Player_Hidden")
        {
            if (frames <= 10)
            {
                frames++;
                transform.Rotate(1, 2, 0);
            }
            else if (frames <= 20)
            {
                frames++;
                transform.Rotate(-1, -2, 0);
            }
        }
    }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player_Hidden")
            {
                frames = 0;
            }
        }
    }
