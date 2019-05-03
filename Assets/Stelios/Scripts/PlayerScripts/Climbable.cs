using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Climbable : MonoBehaviour {

    public Transform endPosition;
    public float height;

    public GameObject TextPrompt;
    public Text TextValue;

    // Use this for initialization

    //void Start () {

    //    TextPrompt = GameObject.Find("Interact Prompt CANVAS");
    //    TextValue = GameObject.Find("Interaction Text").GetComponent<Text>();
    //}

    // Update is called once per frame
    void Update()
    {

        if (TextPrompt.activeSelf)
        {
            TextPrompt.transform.position = GameObject.Find("Peasant_Man").transform.position + new Vector3(0, 2.5f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }
            TextValue.text = "Climb";
            TextPrompt.SetActive(true);
            TextPrompt.transform.position = other.gameObject.transform.position + new Vector3(0, 2, 0);

            other.gameObject.GetComponent<PlayerMovement>().SetJumpDestination(endPosition,height);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            TextValue.text = "Climb";
            TextPrompt.SetActive(true);
            TextPrompt.transform.position = other.gameObject.transform.position + new Vector3(0, 2, 0);

            other.gameObject.GetComponent<PlayerMovement>().SetJumpDestination(endPosition, height);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().isKinematic = true;
            }
            TextPrompt.SetActive(false);
            TextValue.text = "Pick Up";
            other.gameObject.GetComponent<PlayerMovement>().SetJumpDestination(null,0);            
        }
    }
}
