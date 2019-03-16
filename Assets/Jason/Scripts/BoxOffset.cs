using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOffset : MonoBehaviour {

    public GameObject dust;
    public Vector3 temp;
    Vector3 offsetS = new Vector3(0f, 0f, 0.1f);
    Vector3 offsetW = new Vector3(0.1f, 0f, 0f);


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Map South" || other.gameObject.name == "Map West" || other.gameObject.name == "Map North" || other.gameObject.name == "Wall East")
        {
            temp = transform.position;
            Debug.Log("Collision" + temp);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Map North")
        {
            transform.position = temp - new Vector3(0f, 0f, 0.1f);
            Debug.Log(transform.position);
        }

        if (other.gameObject.name == "Map South")
        {
            transform.position = temp + new Vector3(0f, 0f, 0.1f);
            Debug.Log(transform.position);
        }

        if (other.gameObject.name == "Map West")
        {
            transform.position = temp + new Vector3(0.1f, 0f, 0f);
            Debug.Log(transform.position);
        }

        if (other.gameObject.name == "Wall East")
        {
            transform.position = temp - new Vector3(0.1f, 0f, 0f);
            Debug.Log(transform.position);
        }
    }
}
