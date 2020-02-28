using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;
    public float verticalVelocity;
    public float gravity = 14.0f;
    public float currentjumpForce = 10.0f;


    public float baseSpeed = 0.05f;
    public float sprintSpeed = 0.1f;
    public float speed;
    public bool sprinting = false;


        //Calling on the CharacterController Component
    void Start()
    {
        controller = GetComponent<CharacterController>();

        speed = baseSpeed;
    }

    //Calling the PlayerJumping function
    void Update()
    {
        PlayerJumping();
    }

    //Creating the player jumping, and player movement function.
    //If the player is on ground then he is able to jump, depending on the jumpforce and gravity.
    void PlayerJumping()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = currentjumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveVector * Time.deltaTime);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, 0, v) * speed);

        if(Input.GetKey(KeyCode.LeftShift) && !sprinting)
        {
            sprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && sprinting)
        {
            sprinting = false;
        }

        if(sprinting)
        {
            speed = sprintSpeed;
        }

        if(!sprinting)
        {
            speed = baseSpeed;
        }
    }
}
