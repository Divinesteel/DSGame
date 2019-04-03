using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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
        StartCoroutine(WaitTransition());
        PlayerStatus = PlayerStat.Dead;
        //SetKinematicOnRagdoll(false);
        //GetComponent<Animator>().enabled = false;
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<CheckpointCtrl>().LoadCheckpoint();
        Debug.Log("First Load");
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

    public void RevivePlayer()
    {
        //StartCoroutine(WaitTransition());
        gameObject.GetComponent<CheckpointCtrl>().LoadCheckpoint();
        PlayerStatus = PlayerStat.Alive;
        Debug.Log("Second Load");
    }

    IEnumerator WaitTransition()
    {
        int frames = 0;
        while (frames < 601)
        {
            frames++;
            yield return null;
        }
    }
}
