using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public Transform target;
    public Vector3 offset = new Vector3(0f, 18f, 15.5f);

    public Vector3 panLimit = new Vector3(5f, 5f, 5f);

    public float cameraSpeed;
    private float panSpeed;
    public float panBorderThickness;

    public Vector3 MovingCameraPosition;

    public bool isFollowingTarget;

    public bool isReturning;
    private Vector3 StartReturningPos;
    public float returningTimer;

    private float time;

    enum Direction {Up,Down,Left,Right,UpLeft,UpRight,DownLeft,DownRight};

    Direction direction;
      


    void Start()
    {
        transform.position = target.position + offset;
        isFollowingTarget = true;
        panSpeed = 0;
    }

    // Update is called once per frame
    void Update() {

        if (isFollowingTarget && !isReturning)
        {
            transform.position = target.position + offset;
        }


        if (panSpeed > 0 || (!isReturning && (Input.mousePosition.y >= Screen.height - panBorderThickness || 
                            Input.mousePosition.y <= panBorderThickness || 
                            Input.mousePosition.x <= panBorderThickness || 
                            Input.mousePosition.x >= Screen.width - panBorderThickness) 
                         && !Input.GetKey(KeyCode.W)
                         && !Input.GetKey(KeyCode.A)
                         && !Input.GetKey(KeyCode.S)
                         && !Input.GetKey(KeyCode.D)))
        {           

            if (Input.mousePosition.y >= Screen.height - panBorderThickness - 20 && Input.mousePosition.x <= panBorderThickness + 20) 
            {              
                direction = Direction.UpLeft;
                panSpeed = cameraSpeed;
                isFollowingTarget = false;
                time = 0;
            }
            else if (Input.mousePosition.y >= Screen.height - panBorderThickness - 20 && Input.mousePosition.x >= Screen.width - panBorderThickness - 20)
            {
                direction = Direction.UpRight;
                panSpeed = cameraSpeed;
                isFollowingTarget = false;
                time = 0;
            }
            else if (Input.mousePosition.y <= panBorderThickness + 20 && Input.mousePosition.x <= panBorderThickness + 20)
            {
                direction = Direction.DownLeft;
                panSpeed = cameraSpeed;
                isFollowingTarget = false;
                time = 0;
            }
            else if (Input.mousePosition.y <= panBorderThickness + 20 && Input.mousePosition.x >= Screen.width - panBorderThickness - 20)
            {
                direction = Direction.DownRight;
                panSpeed = cameraSpeed;
                isFollowingTarget = false;
                time = 0;
            }

            else if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                direction = Direction.Up;
                panSpeed = cameraSpeed;
                isFollowingTarget = false;
                time = 0;
            }
            else if (Input.mousePosition.y <= panBorderThickness)
            {
                direction = Direction.Down;
                panSpeed = cameraSpeed;
                isFollowingTarget = false;
                time = 0;
            }
            else if (Input.mousePosition.x <= panBorderThickness)
            {
                direction = Direction.Left;
                panSpeed = cameraSpeed;
                isFollowingTarget = false;
                time = 0;
            }
            else if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                direction = Direction.Right;
                panSpeed = cameraSpeed;
                isFollowingTarget = false;
                time = 0;
            }
         
            if (isFollowingTarget == false)
            {
                if(panSpeed > 0)
                {
                    switch (direction)
                    {
                        case Direction.Up:
                            MovingCameraPosition = new Vector3(transform.position.x,
                                                transform.position.y,
                                                transform.position.z - panSpeed * Time.deltaTime);
                            break;
                        case Direction.Down:
                            MovingCameraPosition = new Vector3(transform.position.x,
                                               transform.position.y,
                                               transform.position.z + panSpeed * Time.deltaTime);
                            break;
                        case Direction.Left:
                            MovingCameraPosition = new Vector3(transform.position.x + panSpeed * Time.deltaTime,
                                               transform.position.y,
                                               transform.position.z);
                            break;
                        case Direction.Right:
                            MovingCameraPosition = new Vector3(transform.position.x - panSpeed * Time.deltaTime,
                                                transform.position.y,
                                                transform.position.z);
                            break;
                        case Direction.UpLeft:
                            MovingCameraPosition = new Vector3(transform.position.x + panSpeed * Time.deltaTime,
                                               transform.position.y,
                                               transform.position.z - panSpeed * Time.deltaTime);
                            break;
                        case Direction.UpRight:
                            MovingCameraPosition = new Vector3(transform.position.x - panSpeed * Time.deltaTime,
                                                transform.position.y,
                                                transform.position.z - panSpeed * Time.deltaTime);
                            break;
                        case Direction.DownLeft:
                            MovingCameraPosition = new Vector3(transform.position.x + panSpeed * Time.deltaTime,
                                               transform.position.y,
                                               transform.position.z + panSpeed * Time.deltaTime);
                            break;
                        case Direction.DownRight:
                            MovingCameraPosition = new Vector3(transform.position.x - panSpeed * Time.deltaTime,
                                               transform.position.y,
                                               transform.position.z + panSpeed * Time.deltaTime);
                            break;
                    }

                    MovingCameraPosition.Set(Mathf.Clamp(MovingCameraPosition.x, target.position.x + offset.x - panLimit.x, target.position.x + offset.x + panLimit.x),
                    MovingCameraPosition.y,
                    Mathf.Clamp(MovingCameraPosition.z, target.position.z + offset.z - panLimit.z, target.position.z + offset.z + panLimit.z));

                    panSpeed -= panSpeed * time;
                    time += Time.deltaTime / (float)0.5;

                }
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