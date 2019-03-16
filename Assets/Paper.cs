using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour {

    public GameObject player;
    private PlayerInteract playerInteract;

    public GameObject NoteCanvas;
    public GameObject UICanvas;

    public void Start()
    {
        NoteCanvas.GetComponent<Canvas>().enabled = false;
    }
    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Hidden")
        {
            playerInteract = other.gameObject.GetComponent<PlayerInteract>();
            if (playerInteract.InteractStatus())
            {
                NoteCanvas.GetComponent<Canvas>().enabled = true;
                UICanvas.GetComponent<Canvas>().enabled = false;
                Time.timeScale = 0f;

                //NoteCanvas.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    public void LeaveNote()
    {
        NoteCanvas.GetComponent<Canvas>().enabled = false;
        UICanvas.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 1f;
    }
}
