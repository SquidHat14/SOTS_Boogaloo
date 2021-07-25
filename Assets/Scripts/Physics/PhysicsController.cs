using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : RaycastController
{

    public CollisionInfo collisions;

    public void Move(Vector2 move, bool standingOnPlatform = false, bool jumpInput = false, bool holdingDown = false)
    {
        UpdateRaycastOrigins();

		collisions.Reset();

        if (move.x != 0)
		{
			collisions.faceDir = (int)Mathf.Sign(move.x);
		}

        HorizontalMovement(ref move);

        if(move.y != 0 && collisions.fallingThroughPlatform == false)
        {
            VerticalMovement(ref move, standingOnPlatform, jumpInput, holdingDown);
        }

        transform.Translate(move);

        if (standingOnPlatform)
		{
			collisions.below = true;
		}
    }

    void HorizontalMovement(ref Vector2 move)
    {
        float directionX = collisions.faceDir;
		float rayLength = Mathf.Abs(move.x) + skinWidth;

		if (Mathf.Abs(move.x) < skinWidth)
		{
			rayLength = 2 * skinWidth;
		}

        for (int i = 0; i < horizontalRayCount; i++)
		{
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if(hit)
            {
                if(hit.distance == 0)
                {
                    continue;
                }

                move.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = directionX == -1;
				collisions.right = directionX == 1;
            }
        }
    }

    void VerticalMovement(ref Vector2 move, bool standingOnPlatform, bool jumpInput, bool holdingDown)
    {
        float directionY = Mathf.Sign(move.y);
        float rayLength = Mathf.Abs(move.y) + skinWidth;

        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + move.x);

            
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, directionY * Vector2.up, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {

                if ((hit.collider.tag == "JumpThroughPlatform" || hit.collider.tag ==  "JumpAndFallThroughPlatform") && directionY == 1)
                {
                    continue;
                }


                move.y = (hit.distance - skinWidth) * directionY; // Shortens raylength to the most recently hit objects' distance to player and sets y movement to that distance
                rayLength = hit.distance;

                collisions.below = directionY == -1;
				collisions.above = directionY == 1;
            }
        }
    }

    public bool ProjectMovement(Vector2 move, bool standingOnPlatform)
    {
        float directionY = Mathf.Sign(move.y);
        float colliderSize = collider.size.x * collider.transform.localScale.x;
        float rayLength = Mathf.Abs(move.y) + skinWidth;

        bool ProjectedOnGround = false;

        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.bottomLeft;

            rayOrigin += Vector2.right * (verticalRaySpacing * i + move.x);

            rayOrigin += new Vector2(colliderSize, 0);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, directionY * Vector2.up, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.blue);

            if (hit)
            {
                rayLength = hit.distance;
                ProjectedOnGround = true;
            }
        }

        return ProjectedOnGround;
    }
    
    public void CheckIfOnOneWayPlatform(ref Vector3 move)
    {
        float rayLength = 0.5f + skinWidth;
        bool hitNormalGround = false;  //Here to make sure the player is completely on the one way platform;

        for(int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.bottomLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + move.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, collisionMask);

            if (hit)
            {
                if (hit.collider.tag != "FallThroughPlatform" && hit.collider.tag != "JumpAndFallThroughPlatform")
                {
                    hitNormalGround = true;
                }
                rayLength = hit.distance;
            }
        }

        if(hitNormalGround == false)
        {
            move.y = 0f;
            collisions.fallingThroughPlatform = true;
            Invoke("ResetFallingThroughPlatform", .05f);
        }

    }
    
    void ResetFallingThroughPlatform()
	{
		collisions.fallingThroughPlatform = false;
	}

	public struct CollisionInfo
	{
		public bool above, below;
		public bool left, right;

		public int faceDir;
		public bool fallingThroughPlatform;

		public void Reset()
		{
			above = below = false;
			left = right = false;
		}
	}
}
