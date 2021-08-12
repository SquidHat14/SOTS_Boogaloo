using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhysicsController))]
public class Knockback : MonoBehaviour
{
    [HideInInspector]
    public PhysicsController controller;
    private Vector2 kbVelocity;

    public float velocitySmoothing = .1f;
    void Start()
    {
        controller = GetComponent<PhysicsController>();
    }

    public void ApplyKnockBack(float hitDirection, Vector2 knockbackAngle, float KbSpeed)
    {
        kbVelocity = new Vector2(hitDirection * knockbackAngle.x, knockbackAngle.y);
        kbVelocity *= KbSpeed;
    }

    public void FixedUpdate()
    {
        kbVelocity.x = Mathf.SmoothDamp(kbVelocity.x, 0, ref velocitySmoothing, .1f);
        kbVelocity.y -= 20 * Time.deltaTime;

        controller.Move(kbVelocity * Time.deltaTime);
    }

}
