using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //player transform
    public Vector3 offset = new Vector3(0f, 18f, 15.5f); //camera offset from player

    public Vector3 panLimit = new Vector3(3f, 1f, 2f); // camera pan limit
    public float panSpeed = 10f;
    public float panBorderThickness = 10f;
    private Vector3 pos; //camera global position

    public float transitionDuration = 0.5f;
    public bool lockTarget;


    void Start()
    {
        //set initial camera position relative to player
        pos = target.position + offset;
        lockTarget = false;
    }

    // Update is called once per frame
    void Update()
    {

        //get camera position 
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

        //lock target
        if (Input.GetMouseButton(2))
        {
            //pos = target.position + offset;
            lockTarget = true;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) && pos == target.position + offset) // if player is moving and is not already locked
            {
                pos = target.position + offset; // insta-lock player
            }
            else
            {
                float t = 0.0f;
                t += Time.deltaTime * (Time.timeScale / transitionDuration);
                pos = Vector3.Lerp(pos, target.position + offset, t); // smoothly lock player}
            }
        }
        //camera pan limits
        pos.x = Mathf.Clamp(pos.x, target.position.x + offset.x - panLimit.x, target.position.x + offset.x + panLimit.x);
        pos.y = Mathf.Clamp(pos.y, target.position.y + offset.y - panLimit.y, target.position.y + offset.y + panLimit.y);
        pos.z = Mathf.Clamp(pos.z, target.position.z + offset.z - panLimit.z, target.position.z + offset.z + panLimit.z);

        transform.position = pos; //set new camera position value
    }
}