using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHurtbox : MonoBehaviour
{
    protected BoxCollider2D collider;
    public bool Knockbackable = false;

    public float KnockBackSpeed = 3f;

    public GameObject rootObject;

    protected bool Unhittable = false;

    public Animator animate;

    private SpriteRenderer sprite;

    private Rigidbody2D rigidbody2D;

    protected void Setup()
    {
        collider = GetComponent<BoxCollider2D>();
        animate = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void GetHit(float damage, float hitPositionX, float KbSpeed = 0, bool crit = false)
    {
        if(Knockbackable)
        {
            float hitDirection = collider.bounds.center.x > hitPositionX ? 1 : -1;
            Knockback(hitDirection, KbSpeed == 0 ? KnockBackSpeed : KbSpeed);
        }

        if(!Unhittable)
        {
            try
            {
                animate.SetTrigger("GotHit");
            }
            catch
            {
                Debug.Log("Enemy does not have animator set correctly");
            }

            sprite.color = Color.red;
            Invoke("resetHitColor", .15f);
            TakeDamage(damage, crit);
        }
    }

    protected virtual void TakeDamage(float damage, bool crit)
    {
        DamageUIManager.Instance.DisplayDamage(transform.position, damage, crit);
    }

    protected virtual void Die()
    {
        Unhittable = true;
        Knockbackable = false;
        try
        {
            animate.SetTrigger("Die");
        }
        catch
        {
            Debug.Log("Enemy does not have animator set correctly");
            DestroyAfterDeathAnim();
        }
        //WILL CALL DESTROY AT END OF ANIMATION IF SETUP CORRECTLY
    }

    protected virtual void Knockback(float direction, float speed)
    {
        Debug.Log("HITTING KNOCKBACK ON SOMETHING");
        rigidbody2D.velocity += new Vector2(direction * speed, speed / 4);
        Invoke("resetRBVelocity", .15f);
    }

    private void resetRBVelocity()
    {
        rigidbody2D.velocity = new Vector2(0,0);
    }
    private void finishHitAnimation()
    {
        animate.ResetTrigger("GotHit");
    }

    private void resetHitColor()
    {
        sprite.color = Color.white;
    }

    private void DestroyAfterDeathAnim()
    {
        StartCoroutine(FadeOutAndDie());
    }

    IEnumerator FadeOutAndDie()
    {
        Color TempC = sprite.color;
        float speedFade = 2f;
        while (sprite.color.a > 0f)
        {
            TempC.a -= (0.01f * speedFade);
            sprite.color = TempC;
            Debug.Log("fading: " + sprite.color.a);
            yield return new WaitForSeconds(0.03f);
        }
        Destroy(rootObject);
    }
}
