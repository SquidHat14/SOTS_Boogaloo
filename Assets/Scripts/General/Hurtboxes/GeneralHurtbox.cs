using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHurtbox : MonoBehaviour
{
    protected BoxCollider2D collider;
    public GameObject rootObject;
    public Animator animate;
    private SpriteRenderer sprite;
    private PhysicsController controller;

    private Knockback kbController;

    public bool Knockbackable = false;
    protected bool Unhittable = false;

    protected void Setup()
    {
        collider = GetComponent<BoxCollider2D>();
        animate = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        controller = GetComponent<PhysicsController>();
        kbController = GetComponent<Knockback>();
    }

    public virtual void GetHit(float damage, float hitPositionX, Vector2 knockbackAngle, float KbSpeed, bool crit)
    {
        if(Knockbackable)
        {
            float hitDirection = collider.bounds.center.x > hitPositionX ? 1 : -1;
            kbController.ApplyKnockBack(hitDirection, knockbackAngle, KbSpeed);
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
        UIManager.Instance.DisplayDamage(transform.position, damage, crit);
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
            yield return new WaitForSeconds(0.03f);
        }
        Destroy(rootObject);
    }
}
