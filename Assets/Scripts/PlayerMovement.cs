using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    private Rigidbody rb;
    private Transform tr;

    [SerializeField]
    private float moveSpeed;

    public float walkSpeed = 12f;
    public float sprintSpeed = 20f;
    public float slideSpeed = 10f;
    public float crouchSpeed = 1f;

    private bool isSprinting;
    private bool isSliding;
    public float slideTimer = 0.0f;
    public float slideTimerMax = 1.0f;
    private Vector3 slideDir;

    private Vector3 originalCenter;
    private float originalHeight;

    bool bonkingHead;
    public Transform ceilingCheck;
    public float ceilingDistance = 0.4f;
    public LayerMask ceilingMask;

    bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    void Start()
    {
        transform.tag = "Player";
        tr = transform;
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        originalCenter = controller.center;
        originalHeight = controller.height;
        moveSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float h = originalHeight;

        // Check if object is on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Check if object reaches ceiling
        bonkingHead = Physics.CheckSphere(ceilingCheck.position, ceilingDistance, ceilingMask);

        // Directional movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(isGrounded)
        {
            Vector3 move = (transform.right * x/1.8f) + (transform.forward * z); // move depending on direction player is facing
            controller.Move(move * moveSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 move = (transform.right * x/3) + (transform.forward * z);
            controller.Move(move * moveSpeed * Time.deltaTime);
        }

        // Crouch
        if(Input.GetKey("c") || (bonkingHead && !isSliding))
        {
            h = 0.5f * originalHeight;
            moveSpeed = crouchSpeed;
        }
        // Sprint
        else if(Input.GetKey("left shift"))
        {
            isSprinting = true;
            moveSpeed = sprintSpeed;
        }
        // Walk
        else
        {
            moveSpeed = walkSpeed;
        }

        // Slide
        if(isSprinting && isGrounded && Input.GetKeyDown("v") && !isSliding)
        {
            slideTimer = 0.0f;
            isSliding = true;
        }

        if(isSliding)
        {
            h = 0.05f * originalHeight;
            controller.Move(transform.forward * slideSpeed * Time.deltaTime);

            slideTimer += Time.deltaTime;
            if(slideTimer > slideTimerMax)
            {
                isSliding = false;
                isSprinting = false;
            }
        }

        // Jump
        if(Input.GetButtonDown("Jump") && isGrounded && !bonkingHead)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Reset player gravity velocity upon contact with ground
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Freefall physics -- acceleration
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); // *T.dT again because t^2

        // Smooth crouch-stand transition
        var lastHeight = controller.height;
        controller.height = Mathf.Lerp(controller.height, h, 5f * Time.deltaTime);
        var currPos = tr.position.y;
        currPos += (controller.height - lastHeight)/2f;
    }
}
