using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable][RequireComponent(typeof(PhysicsController))]
public class AIMovement: MonoBehaviour {
  public float accelerationTimeAirborne = .2f;
  public float accelerationTimeGrounded = .18f;
  public float moveSpeed = 4;

  public float gravity;
  public float jumpVelocity;

  [HideInInspector]
  public Vector3 velocity;
  public Animator animate;
  public float velocityXSmoothing;

  public int moveDirection;

  [HideInInspector]
  public PhysicsController controller;
  float Xscale;

  void Awake() 
  {
    controller = GetComponent<PhysicsController>();
    Xscale = this.gameObject.transform.localScale.x;
  }

  public void NextFrame() 
  {
    if (controller.collisions.above) 
    {
      velocity.y = 0;
    }
    if (controller.collisions.below) 
    {
      velocity.y = 0;
    }

    velocity.x = moveDirection * moveSpeed;

    velocity.y += gravity * Time.deltaTime;

    controller.Move(velocity * Time.deltaTime);
  }

  public void setDirection(int direction)
  {
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
}