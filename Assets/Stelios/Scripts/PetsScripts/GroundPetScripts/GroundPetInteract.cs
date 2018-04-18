using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPetInteract : MonoBehaviour {

	public bool isBarking;
	public KeyCode BarkingKey;

	public bool isDigging;
	public KeyCode DiggingKey;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(BarkingKey))
		{
			isBarking = true;
		}
		else if (Input.GetKeyUp(BarkingKey))
		{
			isBarking = false;
		}

		if (Input.GetKeyDown(DiggingKey))
		{
			isDigging = true;
		}
		else if (Input.GetKeyUp(DiggingKey))
		{
			isDigging = false;
		}

	}

    public void Bark()
    {
        isBarking = true;
    }

    public void StopBarking()
    {
        isBarking = false;
    }

    public void Dig()
    {
        isDigging = true;
        Debug.Log(isDigging);
    }

    public void StopDigging()
    {
        isDigging = false;
        Debug.Log(isDigging);
    }
}
