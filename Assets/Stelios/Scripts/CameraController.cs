using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public Transform target;
    public Vector3 offset = new Vector3(0f, 18f, 15.5f);

    public Vector3 panLimit = new Vector3(5f,5f,5f);
    public float panSpeed = 10f;
    public float panBorderThickness = 10f;
    public Vector3 pos;
    private bool isFollowingTarget;


    void Start()
    {
        pos = target.position + offset;
    }

    // Update is called once per frame
    void Update() {

        //if (isFollowingTarget)
        //{
        //    pos = target.position + offset;
        //}
        //else
        //{

        //}

        //Vector3 pos = transform.position;

        //get camera position relative to target


        pos = transform.position;

        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime; //pan camera up
        }
        else if (Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime; //pan camera down
        }
        else if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime; //pan camera right
        }
        else if (Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime; //pan camera left
        }

        else if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
         {
            pos = target.position + offset;
        }

        pos.Set(Mathf.Clamp(pos.x, target.position.x - panLimit.x, target.position.x + panLimit.x),
                pos.y,
                Mathf.Clamp(pos.z, target.position.z - panLimit.z, target.position.z + panLimit.z));

        transform.position = pos; //set new camera position value
    }
        
}