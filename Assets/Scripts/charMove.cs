using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class charMove : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed = 5f;
    public float acceleration = 0.2f;
    private Rigidbody2D rigidBody;

    private Animator animation;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float newSpeed = GetNewSpeed(moveX, rigidBody.velocity.x, this.acceleration);
        rigidBody.velocity = new Vector2(newSpeed, rigidBody.velocity.y);

        animation.SetFloat("speed", Math.Abs(newSpeed));
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
        newSpeed = Math.Min(Math.Abs(newSpeed), maxSpeed);
        return newSpeed * direction;
    }

    int GetDirection(float speed)
    {
        return (speed > 0) ? 1 : -1;
    }
}
