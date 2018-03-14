using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public Transform target;
    public Vector3 offset = new Vector3(0f, 18f, 15.5f);

    public Vector3 panLimit = new Vector3(5f,5f,5f);
    public float panSpeed;
    public float panBorderThickness;

    public Vector3 MovingCameraPosition;

    public bool isFollowingTarget;

    public bool isReturning;
    private Vector3 StartReturningPos;
    public float returningTimer;


    void Start()
    {
        transform.position = target.position + offset;
        isFollowingTarget = true;
    }

    // Update is called once per frame
    void Update() {

        if (isFollowingTarget && !isReturning)
        {
            transform.position = target.position + offset;
        }


        if (!isReturning && (Input.mousePosition.y >= Screen.height - panBorderThickness || 
                            Input.mousePosition.y <= panBorderThickness || 
                            Input.mousePosition.x <= panBorderThickness || 
                            Input.mousePosition.x >= Screen.width - panBorderThickness) 
                         && !Input.GetKey(KeyCode.W)
                         && !Input.GetKey(KeyCode.A)
                         && !Input.GetKey(KeyCode.S)
                         && !Input.GetKey(KeyCode.D))
        {
            if(Input.mousePosition.y >= Screen.height - panBorderThickness && Input.mousePosition.x <= panBorderThickness) 
            {
                isFollowingTarget = false;
                MovingCameraPosition = new Vector3(transform.position.x + panSpeed * Time.deltaTime,
                                                transform.position.y,
                                                transform.position.z - panSpeed * Time.deltaTime);
            }
            else if (Input.mousePosition.y >= Screen.height - panBorderThickness && Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                isFollowingTarget = false;
                MovingCameraPosition = new Vector3(transform.position.x - panSpeed * Time.deltaTime,
                                                transform.position.y,
                                                transform.position.z - panSpeed * Time.deltaTime);
            }
            else if (Input.mousePosition.y <= panBorderThickness && Input.mousePosition.x <= panBorderThickness)
            {
                isFollowingTarget = false;
                MovingCameraPosition = new Vector3(transform.position.x + panSpeed * Time.deltaTime,
                                                transform.position.y,
                                                transform.position.z + panSpeed * Time.deltaTime);
            }
            else if (Input.mousePosition.y <= panBorderThickness && Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                isFollowingTarget = false;
                MovingCameraPosition = new Vector3(transform.position.x - panSpeed * Time.deltaTime,
                                                transform.position.y,
                                                transform.position.z + panSpeed * Time.deltaTime);
            }

            else if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                isFollowingTarget = false;
                MovingCameraPosition = new Vector3(transform.position.x,
                                                transform.position.y,
                                                transform.position.z - panSpeed * Time.deltaTime);
            }
            else if (Input.mousePosition.y <= panBorderThickness)
            {
                isFollowingTarget = false;
                MovingCameraPosition = new Vector3(transform.position.x,
                                                transform.position.y,
                                                transform.position.z + panSpeed * Time.deltaTime);
            }
            else if (Input.mousePosition.x <= panBorderThickness)
            {
                isFollowingTarget = false;
                MovingCameraPosition = new Vector3(transform.position.x + panSpeed * Time.deltaTime,
                                                transform.position.y,
                                                transform.position.z);
            }
            else if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                isFollowingTarget = false;
                MovingCameraPosition = new Vector3(transform.position.x - panSpeed * Time.deltaTime,
                                                transform.position.y,
                                                transform.position.z);
            }

            MovingCameraPosition.Set(Mathf.Clamp(MovingCameraPosition.x, target.position.x + offset.x - panLimit.x, target.position.x + offset.x + panLimit.x),
                    MovingCameraPosition.y,
                    Mathf.Clamp(MovingCameraPosition.z, target.position.z + offset.z - panLimit.z, target.position.z + offset.z + panLimit.z));

            if (isFollowingTarget == false)
            {
                transform.position = MovingCameraPosition; 
            }
        }
       

        if (isReturning || (Input.GetKeyDown(KeyCode.Space) && isFollowingTarget == false))
        {
            //transform.position = target.position + offset; 
            //isFollowingTarget = true;

            if (transform.position != target.position + offset)
            {
                if (!isReturning)
                {
                    isFollowingTarget = true;
                    isReturning = true;
                    StartReturningPos = transform.position;
                    returningTimer = 0;
                }

                transform.position = Vector3.Lerp(StartReturningPos, target.position + offset, -Mathf.Pow(2,-returningTimer + 1) +2/*-(returningTimer - 1) * (returningTimer - 1) + 1*/);
                returningTimer += Time.deltaTime / 0.3f;
            }
            else
            {
                isReturning = false;
            }
        }   
        
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || 
           Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) && transform.position != target.position + offset) 
        {
            if (!isReturning)
            {
                isFollowingTarget = true;
                isReturning = true;
                StartReturningPos = transform.position;
                returningTimer = 0;
            }
        }
        
    }
    
}