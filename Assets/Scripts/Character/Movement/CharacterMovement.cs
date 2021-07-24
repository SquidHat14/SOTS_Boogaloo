using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(PhysicsController))]
public class CharacterMovement : MonoBehaviour
{    
    public float moveSpeed = 4;

    [Range (0,15)]
    public int coyoteTimeFrameLimit = 3;
    private int coyoteTimeCurrentFrame = 0;
    public float gravity;
    public float jumpVelocity;
    public float minJumpVelocity;

    [HideInInspector]
    public Vector3 velocity;
    public Animator animate;
    public float velocityXSmoothing;

    private bool holdingJump;

    private bool holdingDown;
    private float inputX;
    private bool alreadyJumped = true;

    [HideInInspector]
    public PhysicsController controller;
    float Xscale;


    void Start()
    {
        controller = GetComponent<PhysicsController>();
        animate = GetComponent<Animator>();
        Xscale = transform.localScale.x;
    }

    void FixedUpdate()
    {
        float targetVelocityX = inputX * moveSpeed;

        if (controller.collisions.above)
        {
            velocity.y = 0;
        }
        if (controller.collisions.below)
        {
            velocity.y = 0;
            coyoteTimeCurrentFrame = 0;
            alreadyJumped = false;
        }

        if (holdingJump && (controller.collisions.below || (coyoteTimeCurrentFrame < coyoteTimeFrameLimit && alreadyJumped == false)))
        {
            velocity.y = jumpVelocity;
            alreadyJumped = true;
        }

        if (!holdingJump && !controller.collisions.below && velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }

        coyoteTimeCurrentFrame++;

        velocity.x = targetVelocityX;

        velocity.y += gravity * Time.deltaTime;

        if(holdingDown && holdingJump && controller.collisions.below)
        {
            controller.CheckIfOnOneWayPlatform(ref velocity);
        }
        
        controller.Move(velocity * Time.deltaTime, false, holdingJump, holdingDown);

        if (inputX < 0)
        {
            transform.localScale = new Vector2(-Xscale, transform.localScale.y);
        }
        else if(inputX > 0)
        {
            transform.localScale = new Vector2(Xscale, transform.localScale.y);
        }
        
        animate.SetInteger("xSpeed", (int) velocity.x);
    }


   private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        holdingJump = Input.GetKey("w") || Input.GetKey(KeyCode.Space);

        holdingDown = Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow);
    }
}
