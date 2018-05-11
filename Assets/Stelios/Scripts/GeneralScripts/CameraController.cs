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

    private bool lockUp;
    private bool lockDown;
    private bool lockLeft;
    private bool lockRight;
    private bool checkLock;

    enum Direction {Up,Down,Left,Right,UpLeft,UpRight,DownLeft,DownRight};

    Direction direction;

    public bool invertCamera;

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
            checkLock = false;

            if (Input.mousePosition.y >= Screen.height - panBorderThickness - 20 && Input.mousePosition.x <= panBorderThickness + 20) 
            {
                ControlCameraMovement(Direction.DownRight, Direction.UpLeft,lockDown,lockUp,lockRight,lockLeft);
            }
            else if (Input.mousePosition.y >= Screen.height - panBorderThickness - 20 && Input.mousePosition.x >= Screen.width - panBorderThickness - 20)
            {
                ControlCameraMovement(Direction.DownLeft, Direction.UpRight,lockDown,lockUp,lockLeft,lockRight);
            }
            else if (Input.mousePosition.y <= panBorderThickness + 20 && Input.mousePosition.x <= panBorderThickness + 20)
            {
                ControlCameraMovement(Direction.UpRight, Direction.DownLeft,lockUp,lockDown,lockRight,lockLeft);
            }
            else if (Input.mousePosition.y <= panBorderThickness + 20 && Input.mousePosition.x >= Screen.width - panBorderThickness - 20)
            {
                ControlCameraMovement(Direction.UpLeft, Direction.DownRight,lockUp,lockDown,lockLeft,lockRight);
            }

            else if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                ControlCameraMovement(Direction.Down, Direction.Up,lockDown,lockUp);
            }
            else if (Input.mousePosition.y <= panBorderThickness)
            {
                ControlCameraMovement(Direction.Up, Direction.Down,lockUp,lockDown);
            }
            else if (Input.mousePosition.x <= panBorderThickness)
            {
                ControlCameraMovement(Direction.Right, Direction.Left,lockRight,lockLeft);
            }
            else if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                ControlCameraMovement(Direction.Left, Direction.Right,lockLeft,lockRight);
            }

            if (isFollowingTarget == false && checkLock)
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
                        default:
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
    private void ControlCameraMovement(Direction a, Direction b, bool aa, bool ba, bool ab = false, bool bb = false)
    {
        if (invertCamera)
        {
            if(!aa && !ab)
            {
                direction = a;
                checkLock = true;
            }    
        }
        else
        {
            if(!ba && !bb)
            {
                direction = b;
                checkLock = true;
            }          
        }
        panSpeed = cameraSpeed;
        isFollowingTarget = false;
        time = 0;
    }

    public void LockUp()
    {
        lockUp = true;
    }
    public void UnlockUp()
    {
        lockUp = false;
    }
    public void LockDown()
    {
        lockDown = true;
    }
    public void UnlockDown()
    {
        lockDown = false;
    }
    public void LockLeft()
    {
        lockLeft = true;
    }
    public void UnlockLeft()
    {
        lockLeft = false;
    }
    public void LockRight()
    {
        lockRight = true;
    }
    public void UnlockRight()
    {
        lockRight = false;
    }

    public void SetPanVertical(float z)
    {
        panLimit.z = z;
    }
    public void SetPanHorizontal(float x)
    {
        panLimit.x = x;
    }
    public void SetPanVerticalToDefault()
    {
        panLimit.z = 3;
    }
    public void SetPanHorizontalToDefaultfloat()
    {
        panLimit.x = 3;
    }

}