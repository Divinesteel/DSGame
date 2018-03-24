using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody rb;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Vector3 lookToward;

    private Camera mainCamera;

    private Animator anim;

    float time;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
        time = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float lh = Input.GetAxis("Horizontal");
        float lv = Input.GetAxis("Vertical");

        moveInput = new Vector3(lh, 0f, lv);
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;

        Quaternion cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);
        lookToward = cameraRelativeRotation * moveInput;

        if (lh + lv == 0)
        {
            time = 0;
        }

        if(moveInput.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookToward), time);
            //Ray lookRay = new Ray(transform.position, lookToward);
            time += Time.deltaTime / 1;
            //transform.LookAt(lookRay.GetPoint(1));


        }

        

        moveVelocity = transform.forward * moveSpeed * Mathf.Clamp(moveInput.magnitude,0,1);

        anim.SetFloat("Forward", moveVelocity.magnitude / moveSpeed);    


    }

    void FixedUpdate()
    {
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
               
    }
}
