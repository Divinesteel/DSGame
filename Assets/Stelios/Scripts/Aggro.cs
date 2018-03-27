using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Aggro : MonoBehaviour {

    private NavMeshAgent agent;
    public Transform PlayerTransform;
    public bool IsTargetInRange;
    private Collider[] colliders;
    public Transform startingPosition;
    public bool isReturning;
    public float chaseDistance;
    Vector3 dest;

	// Use this for initialization
	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        colliders = GetComponents<CapsuleCollider>();
        isReturning = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (IsTargetInRange)
        {
            Debug.DrawRay(startingPosition.position,transform.position - startingPosition.position , Color.red);
            dest = new Vector3((transform.position - startingPosition.position).x,
                (transform.position - startingPosition.position).y,
                (transform.position - startingPosition.position).z);

            if (dest.magnitude < chaseDistance  && !isReturning)
            {
                agent.destination = PlayerTransform.transform.position;
            }
            else
            {
                isReturning = true;
                foreach (var collider in colliders)
                {
                    collider.enabled = false;
                }
                
                agent.destination = startingPosition.position;
                IsTargetInRange = false;
            }
            
        }
        
        if (agent.velocity.magnitude == 0 && isReturning)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, startingPosition.rotation, Time.deltaTime * 5);
            if (Vector3.Angle(transform.forward, startingPosition.forward) < 1.1)
            {
                isReturning = false;
            }
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }       

        }
        
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            IsTargetInRange = true;
        }


    }

}

