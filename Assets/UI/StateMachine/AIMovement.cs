using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable][RequireComponent(typeof(PhysicsController))]
public class AIMovement: MonoBehaviour {
  public float accelerationTimeAirborne = .2f;
  public float accelerationTimeGrounded = .18f;
  public float moveSpeed = 4;

  [Range(0, 15)]
  public int coyoteTimeFrameLimit = 3;
  private int coyoteTimeCurrentFrame = 0;
  public float gravity;
  public float jumpVelocity;

  [HideInInspector]
  public Vector3 velocity;
  public Animator animate;
  public float velocityXSmoothing;

  private bool holdingJump;
  private int moveDirection;
  private bool alreadyJumped = true;
  private bool inCustcene = false;

  [HideInInspector]
  public PhysicsController controller;
  float Xscale;

  void Awake() {
    controller = GetComponent <PhysicsController> ();
    Xscale = this.gameObject.transform.localScale.x;
  }

  void FixedUpdate() {
    float targetVelocityX = moveDirection * moveSpeed;

    if (controller.collisions.above) {
      velocity.y = 0;
    }
    if (controller.collisions.below) {
      velocity.y = 0;
      coyoteTimeCurrentFrame = 0;
      alreadyJumped = false;
    }

    if (holdingJump && (controller.collisions.below || (coyoteTimeCurrentFrame < coyoteTimeFrameLimit && alreadyJumped == false))) {
      velocity.y = jumpVelocity;
      alreadyJumped = true;
    }
    coyoteTimeCurrentFrame++;
    velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded: accelerationTimeAirborne);

    velocity.y += gravity * Time.deltaTime;

    controller.Move(velocity * Time.deltaTime);

    holdingJump = false;
  }

  public void Jump() {
    holdingJump = true;
  }

  public void setDirection(int direction) {
    moveDirection = direction;
    flipSprite(direction);
  }

  public void turnAround()
  {
    moveDirection *= -1;
    flipSprite(moveDirection);
  }

  void flipSprite(int direction)
  {
    if(direction != 0)
    {
        this.gameObject.transform.localScale = new Vector2(Xscale * direction, transform.localScale.y);
    }
  }

  //TO DO : get collision data as a given Vector 2.  Will be used to determine when to jump vs turn around and platform patrol.
}