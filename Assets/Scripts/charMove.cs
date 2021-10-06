using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class charMove : MonoBehaviour
{
    public float speed = 5f;
    public float maxWalkSpeed = 5f;
    public float maxSprintSpeed = 10f;
    public float acceleration = 0.3f;
    private Rigidbody2D rigidBody;
    private CameraMovement camera;

    private float oldInput = 0f;

    private Animator animation;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        camera = (CameraMovement)GameObject.FindGameObjectWithTag("MainCamera").GetComponent("CameraMovement");
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float newSpeed = GetNewSpeed(moveX, rigidBody.velocity.x, this.acceleration);
        rigidBody.velocity = new Vector2(newSpeed, rigidBody.velocity.y);
        if (newSpeed != 0)
            Debug.Log("newSpeed " + newSpeed + " input " + moveX);
        //animation.SetFloat("speed", Math.Abs(newSpeed));
        // flip sprite if necessary
        if ((newSpeed < -0.1f && transform.localScale.x > 0)
        || (newSpeed > 0.1f && transform.localScale.x < 0))
            
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
           
        }

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
