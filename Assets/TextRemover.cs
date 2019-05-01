using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRemover : MonoBehaviour {

    public GameObject TextPrompt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            TextPrompt.SetActive(false);
        }
    }
}
