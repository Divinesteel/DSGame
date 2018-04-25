using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverBarrel : MonoBehaviour {

    private bool exploded;
    private bool isBarrelAtTriggerPosition;

    public ParticleSystem explosion;

    void Update()
    {
        if (exploded)
        {
            if (!explosion.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void setBarelReady()
    {
        isBarrelAtTriggerPosition = true;
    }

    public bool getBarelStatus()
    {
        return isBarrelAtTriggerPosition;
    }

    public void Explode()
    {
        explosion.Play();
        exploded = true;
    }

}
