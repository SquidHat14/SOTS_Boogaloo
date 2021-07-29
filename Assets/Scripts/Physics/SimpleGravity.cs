using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsController))]
public class SimpleGravity : MonoBehaviour
{

    public float gravity = -15f;
    private PhysicsController physicsController;
    private Vector2 velocity;
    void Start()
    {
        physicsController = GetComponent<PhysicsController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (physicsController.collisions.above || physicsController.collisions.below)
        {
            velocity.y = 0;
        }

        velocity.y += gravity * Time.deltaTime;

        physicsController.Move(velocity * Time.deltaTime);
    }
}
