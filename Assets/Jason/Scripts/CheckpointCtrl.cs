using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCtrl : MonoBehaviour {
    
    public GameObject[] Enemies;

    public GameObject groundComp;
    public GameObject flyingComp;
    public GameObject player;
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private Vector3 groundPos;
    [SerializeField] private Vector3 flyingPos;

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

        foreach (GameObject Enemy in Enemies)
        {
            Enemy.GetComponentInChildren<Enemy_0>().SaveState();
        }
    }

	public void LoadCheckpoint()
	{
        player.transform.position = playerPos;
        groundComp.transform.position = groundPos;
        flyingComp.transform.position = flyingPos;

        Debug.Log("Checkpoint " + checkpointIndex + " loaded.");

        foreach (GameObject Enemy in Enemies)
        {
            Enemy.GetComponentInChildren<Enemy_0>().ResetState();
        }
        
    }
}
