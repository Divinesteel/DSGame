using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed;
	public bool invertHorizontal;
	public bool invertVertical;
	public Transform endPos;
	public float jumpHeight;

    private Collider col;
	private Rigidbody rb;
	private Vector3 moveInput;
	private Vector3 moveVelocity;
	private Vector3 lookToward;
	private Camera mainCamera;
	private Animator anim;
	private bool isAnimJumping;
	private float time;

	private float rotateTimer;
	private float jumpTimer;
	private bool isRot;
	private bool isMov;

    public Vector3 StartPos;
    private bool isJumping;

    private Vector3 EndPosValue;
    private float jumpHeightValue;

    // Use this for initialization
    void Start () {
        col = GetComponent<Collider>();
		rb = GetComponent<Rigidbody>();
		mainCamera = Camera.main;
		anim = GetComponent<Animator>();
		time = 0;
		rotateTimer = 0;
        jumpTimer = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.Space) && endPos != null && jumpHeight != 0)
        {
            isJumping = true;
            isRot = true;
            EndPosValue = new Vector3(endPos.position.x, endPos.position.y, endPos.position.z);
            jumpHeightValue = jumpHeight;
            rb.isKinematic = true;
            col.enabled = false;

            //set to exit because of exitTrigger
            jumpHeight = 0;
            endPos = null;
        }

        if (isJumping)
        {
            Jump();
        }
		
		Move();
	}

	void FixedUpdate()
	{
			rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
			   
	}

	void Move()
	{
		float lh = Input.GetAxis("Horizontal");
		float lv = Input.GetAxis("Vertical");

		moveInput = new Vector3(lh * InvertAxis(invertHorizontal), 0f, lv * InvertAxis(invertVertical));



		Vector3 cameraForward = mainCamera.transform.forward;
		cameraForward.y = 0;

		Quaternion cameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, cameraForward);
		lookToward = cameraRelativeRotation * moveInput;

		if (lh + lv == 0)
		{
			time = 0;
		}

		if (moveInput.sqrMagnitude > 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookToward), time);
			//Ray lookRay = new Ray(transform.position, lookToward);
			time += Time.deltaTime / 1;
			//transform.LookAt(lookRay.GetPoint(1));

		}

		moveVelocity = transform.forward * moveSpeed * Mathf.Clamp(moveInput.magnitude, 0, 1);

		anim.SetFloat("Forward", moveVelocity.magnitude / moveSpeed);
	}

	private int InvertAxis(bool direction)
	{
		if (direction) return -1;
		else return 1;
	}

	void Jump()
	{
		isAnimJumping = false;   

        if (isRot)
		{
				Vector3 endPosition0Y = new Vector3(EndPosValue.x, transform.position.y, EndPosValue.z);
				Vector3 relativePos = endPosition0Y - transform.position;
				Quaternion endRotation = Quaternion.LookRotation(relativePos);

				transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, rotateTimer);

				rotateTimer += (Time.deltaTime * Time.deltaTime) / 0.1f;

			if (Vector3.Angle(transform.forward,relativePos) < 1)
			{
				isRot = false;
				rotateTimer = 0;
				isMov = true;
                isAnimJumping = true;
                StartPos = new Vector3 (transform.position.x,transform.position.y,transform.position.z);               
            }
		}

		if (isMov)
		{
            float yOffset = jumpHeightValue * (jumpTimer - jumpTimer * jumpTimer);
            transform.position = Vector3.Lerp(StartPos, EndPosValue, jumpTimer) + yOffset * Vector3.up;
            jumpTimer += Time.deltaTime / 1f;
            Debug.Log(jumpTimer);
            if (jumpTimer >= 1)
            {
                isMov = false;               
                jumpTimer = 0;
                rb.isKinematic = false;
                col.enabled = true;
            }
		}

        anim.SetBool("IsJumping", isAnimJumping);
    }



	public void SetJumpDestination(Transform pos,float h)
	{
		endPos = pos;
		jumpHeight = h;
	}

}
