using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckpointCtrl : MonoBehaviour {

    public static CheckpointCtrl CPC;

    public GameObject[] Enemies;

    public GameObject groundComp;
    public GameObject flyingComp;
    public GameObject player;
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private Vector3 groundPos;
    [SerializeField] private Vector3 flyingPos;
    [SerializeField] private Transform tempGroundDest;
    [SerializeField] private Transform tempFlyingDest;
    [SerializeField] private bool tempGroundFollow;
    [SerializeField] private bool tempFlyingFollow;

    int checkpointIndex = 0;

    void Awake()
    {
        //Singleton pattern
        if (CPC == null)
        {
            DontDestroyOnLoad(gameObject);
            CPC = this;
        }
        else if (CPC != this)
        {
            Destroy(gameObject);
        }
    }

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

        tempGroundDest = groundComp.GetComponent<MoveNavGroundCompanion>().target;
        tempFlyingDest = flyingComp.GetComponent<MoveNavFlightCompanion>().target;

        tempGroundFollow = groundComp.GetComponent<MoveNavGroundCompanion>().isFollowingTarget;
        tempFlyingFollow = flyingComp.GetComponent<MoveNavFlightCompanion>().isFollowingTarget;

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

        groundComp.GetComponent<NavMeshAgent>().enabled = false;
        flyingComp.GetComponent<NavMeshAgent>().enabled = false;

        groundComp.transform.position = groundPos;
        flyingComp.transform.position = flyingPos;

        groundComp.GetComponent<NavMeshAgent>().enabled = true;
        flyingComp.GetComponent<NavMeshAgent>().enabled = true;

        groundComp.GetComponent<MoveNavGroundCompanion>().target = tempGroundDest;
        flyingComp.GetComponent<MoveNavFlightCompanion>().target = tempFlyingDest;

        groundComp.GetComponent<MoveNavGroundCompanion>().isFollowingTarget = tempGroundFollow;
        flyingComp.GetComponent<MoveNavFlightCompanion>().isFollowingTarget = tempFlyingFollow;

        Debug.Log("Checkpoint " + checkpointIndex + " loaded.");

        foreach (GameObject Enemy in Enemies)
        {
            Enemy.GetComponentInChildren<Enemy_0>().ResetState();
        }
    }
}
