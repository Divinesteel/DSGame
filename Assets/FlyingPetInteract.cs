using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPetInteract : MonoBehaviour
{

    public bool IsSinging;
    public KeyCode SingingKey;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(SingingKey))
        {
            IsSinging = true;
        }
        else if (Input.GetKeyUp(SingingKey))
        {
            IsSinging = false;
        }
    }

    public void Sing()
    {
        IsSinging = true;
    }

    public void StopSinging()
    {
        IsSinging = false;
    }
}
