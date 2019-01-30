using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    CheckpointCtrl checkpoint;

    public enum PlayerStat {Alive, Dead};
    public PlayerStat PlayerStatus;

	// Use this for initialization
	void Start () {
        PlayerStatus = PlayerStat.Alive;
        //SetKinematicOnRagdoll(true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void KillPlayer()
    {
        //PlayerStatus = PlayerStat.Dead;
        //SetKinematicOnRagdoll(false);
        //GetComponent<Animator>().enabled = false;
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CheckpointCtrl>().LoadCheckpoint();
    }

    public PlayerStat GetPlayerStatus()
    {
        return PlayerStatus;
    }

    private void SetKinematicOnRagdoll(bool value)
    {
        if(value == false)
        {
            CapsuleCollider[] capsules = GetComponentsInChildren<CapsuleCollider>();
            foreach (CapsuleCollider cc in capsules)
            {
                cc.enabled = true;
            }
        }

        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = value;
        }
    }
}
