using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class charMove : MonoBehaviour
{
    public float speed = 5f;
    public float maxWalkSpeed = 5f;
    public float maxSprintSpeed = 10f;
    public float acceleration = 1f;
    private Rigidbody2D rigidBody;
    private CameraMovement camera;
    public Animator animator;
    public CharacterController2D controller;
    private float oldInput = 0f;
    bool crouch = false;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        camera = (CameraMovement)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("CameraMovement");
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float newSpeed = GetNewSpeed(moveX, rigidBody.velocity.x, this.acceleration);
        animator.SetFloat("Speed", Mathf.Abs(newSpeed));
        rigidBody.velocity = new Vector2(newSpeed, rigidBody.velocity.y);
        if (newSpeed != 0)
            //Debug.Log("newSpeed " + newSpeed + " input " + moveX);
        //animation.SetFloat("speed", Math.Abs(newSpeed));
        // flip sprite if necessary
        if ((newSpeed < -0.1f && transform.localScale.x > 0)
        || (newSpeed > 0.1f && transform.localScale.x < 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);         
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool IsCrouching)
    {
        animator.SetBool("IsCrouching", IsCrouching);
    }

    private void FixedUpdate()
    {
        controller.Move(crouch, jump);
        jump = false;
    }

    float GetNewSpeed(float input, float velocity, float acceleration)
    {
        float newSpeed = velocity + (acceleration * input);
        int direction = GetDirection(newSpeed);
        newSpeed = Math.Min(Math.Abs(newSpeed), Input.GetKey("left shift") ? maxSprintSpeed : maxWalkSpeed);
        if (isStopping(input) && Math.Abs(newSpeed) <= maxWalkSpeed)
        {
            newSpeed = 0;
            camera.isSprinting = false;
        }
        else
        {
            camera.isSprinting = true;
        }
        oldInput = input;
        return newSpeed * direction;
    }

    int GetDirection(float speed)
    {
        return (speed > 0) ? 1 : -1;
    }

    bool isStopping(float input)
    {
        return Math.Abs(input) < Math.Abs(oldInput) || input == 0;
    }
}
