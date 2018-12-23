using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveMechanism : MonoBehaviour
{

    public bool button1Pressed;
    public bool button2Pressed;

    public CaveButton button1;
    public CaveButton button2;

    public Animator anim;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateButtonsStatuses();
        if (button1Pressed && button2Pressed)
        {
            anim.SetBool("ButtonsArePressed", true); //Sets Animator "ButtonsArePressed" To True
        }
    }

    void UpdateButtonsStatuses()
    {
        button1Pressed = button1.GetButtonStatus();
        button2Pressed = button2.GetButtonStatus();
    }

}
