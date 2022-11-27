using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float crouchSpeed = 6f;
    public float runSpeed = 12f;
    private float currentSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public bool sprinting;
    public bool crouching;

    [SerializeField]
    private Animator camAnim;

    [SerializeField]
    private AudioClip jumpClip;

    public Transform grounCheck;
    public float groundDistance = 0.4f;
    public LayerMask grounMask;

    [SerializeField]
    Vector3 velocity;
    [SerializeField]
    bool isGrounded;
    AudioSource As;


    private void Start()
    {
        As = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(grounCheck.position, groundDistance, grounMask);
        sprinting = Input.GetKey(KeyCode.LeftShift);

        if (isGrounded && !sprinting)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (!crouching)
                {
                    Crouch();
                }
                else
                {
                    Stand();
                }
            }
        }
        else
        {
            Stand();
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (!crouching)
        {
            currentSpeed = (sprinting) ? runSpeed : speed;
        }
        else
        {
            currentSpeed = crouchSpeed;
        }
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (!isGrounded && velocity.y >= -15f)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);

        camAnim.SetBool("Moving", (Mathf.Abs(move.magnitude) >= 1f) && !crouching);
        camAnim.SetBool("Sprinting", sprinting);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        camAnim.SetTrigger("Jump");
        As.pitch = Random.Range(0.9f, 1.2f);
        As.PlayOneShot(jumpClip);
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void Crouch()
    {
        crouching = true;
        controller.height = 1.25f;
        controller.center = new Vector3(0f, 0.25f, 0f);
    }

    void Stand()
    {
        if(!crouching)
        {
            return;
        }
        crouching = false;
        controller.height = 2f;
        controller.center = new Vector3(0f, 0f, 0f);
    }
}
