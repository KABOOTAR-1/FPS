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

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
    
  
    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckGround();
        Jump();
    }
}
