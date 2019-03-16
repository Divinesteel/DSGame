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
        if (IsPlayer(other))
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
            if (IsPlayer(other))
            {
                frames = 0;
            }
        }

    bool IsPlayer(Collider other)
    {
        try
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    }
