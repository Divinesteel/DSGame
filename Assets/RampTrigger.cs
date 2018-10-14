using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class RampTrigger : MonoBehaviour
{

    public GameObject rampGround;
    public GameObject weight;

    Animator animation;
    Collider weightCollider;
    public GameObject block;

    public bool isWeightRemoved;

    // Use this for initialization
    void Start()
    {
        animation = GetComponent<Animator>();
        isWeightRemoved = false;
}

    // Update is called once per frame
    void Update()
    { 
        if (isWeightRemoved)
        {
            animation.SetTrigger("RampTrigger");
            block.GetComponent<BoxCollider>().enabled = false;
        }

    }

    public void OnTriggerExit(Collider weightCollider)
    {
        if (weightCollider.gameObject.name == "Weight")
        {
            isWeightRemoved = true;
        }
        
    }


}

