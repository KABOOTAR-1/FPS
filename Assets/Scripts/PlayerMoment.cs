using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoment : MonoBehaviour
{

    [SerializeField]
    CharacterController controller;
    public float speed = 2f;
    public float height = 2f;
    [SerializeField]
    float gravity = -9.8f;
    Vector3 velocity;
    public Transform checkground;
    public float groundDistance = 0.2f;
    public LayerMask Ground;
    bool isGrounded;
    Vector3 direction;
    public float playerheight = 0;
    Vector3 centre;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerheight = controller.height;
        centre = controller.center;
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        direction = transform.right * x + transform.forward * y;

        if (controller != null)
        {
            controller.Move(direction * Time.deltaTime * speed);
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(checkground.position, groundDistance, Ground);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
    }

    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(height * -2 * gravity);
        }
    }
    
  void Crouch()
    {
        if ( Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = playerheight / 2;
        }
        else
        {
            controller.height = Mathf.Lerp(controller.height, playerheight, Time.deltaTime * 30f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Crouch();
        Movement();
        CheckGround();
        Jump();
    }
}
