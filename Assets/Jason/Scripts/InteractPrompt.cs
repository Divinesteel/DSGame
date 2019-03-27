using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPrompt : MonoBehaviour {

    public GameObject TextPrompt;
    public Text TextValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            TextPrompt.SetActive(true);
            TextPrompt.transform.position = transform.position + new Vector3(0, 2, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            TextPrompt.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        TextPrompt.SetActive(false);
    }
}
