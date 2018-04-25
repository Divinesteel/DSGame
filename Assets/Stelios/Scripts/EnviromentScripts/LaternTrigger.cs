using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaternTrigger : FlyingPetClickInteractable {

    private Animation animation;
    private bool hasBeenTriggered;

    public Rigidbody[] DetonateRocks;
    public BridgeTrigger bridgeTrigger;

    public RiverBarrel riverBarrel;

    // Use this for initialization
    void Start () {
        animation = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {

        if (base.pet != null && !hasBeenTriggered && riverBarrel.getBarelStatus())
        {
            if (base.pet.interact == true)
            {
                animation.Play("Animation_LaternDrop");
                hasBeenTriggered = true;
            }
        }
    }

    public void Explode()
    {
        riverBarrel.Explode();
        Destroy(gameObject);
        bridgeTrigger.DestroyLeftColumn();

        foreach (Rigidbody rb in DetonateRocks)
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)));
            rb.AddTorque(new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100)));
        }
    }

}
