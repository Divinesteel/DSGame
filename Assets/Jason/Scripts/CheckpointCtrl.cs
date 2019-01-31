using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCtrl : MonoBehaviour {

    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Enemy5;

    public GameObject groundComp;
    public GameObject flyingComp;
    public GameObject player;
    public Vector3 playerPos;
    public Vector3 groundPos;
    public Vector3 flyingPos;

    int checkpointIndex = 0;

	// Use this for initialization
	void Start () {
        SaveCheckpoint();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadCheckpoint();
        }

    }

	public void SaveCheckpoint()
	{
        playerPos = player.transform.position;
        groundPos = groundComp.transform.position;
        flyingPos = flyingComp.transform.position;
        checkpointIndex++;
        Debug.Log("Checkpoint " + checkpointIndex + " saved.");
	}

	public void LoadCheckpoint()
	{
		player.transform.position = playerPos;
        groundComp.transform.position = groundPos;
        flyingComp.transform.position = flyingPos;


        Debug.Log ("Checkpoint " + checkpointIndex + " loaded.");

        Enemy1.GetComponent<Enemy_0>().ResetState();
        Enemy2.GetComponent<Enemy_0>().ResetState();
        Enemy3.GetComponent<Enemy_0>().ResetState();
        Enemy4.GetComponent<Enemy_0>().ResetState();
        Enemy5.GetComponent<Enemy_0>().ResetState();
    }
}
