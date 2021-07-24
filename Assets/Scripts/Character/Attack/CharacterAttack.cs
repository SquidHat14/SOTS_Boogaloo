using UnityEngine;

public class CharacterAttack : MonoBehaviour
{    
    private bool attackInput = false;
    private bool attacking = false;
    
    public float damage = 10f;
    
    private Animator animate;

    public BoxCollider2D hitBox;

    private PlayerHitbox pHB;

    void Start()
    {
        animate = GetComponent<Animator>();
        if(!hitBox)
        {
            Debug.LogError("Attack Needs a Hitbox");
        }
        pHB = hitBox.transform.gameObject.GetComponent<PlayerHitbox>();
        DisableHitbox();
    }

    void FixedUpdate()
    {
        if(attackInput && !attacking)
        {
            attacking = true;
            animate.SetBool("Attacking", true);
        }
    }

    void Update()
    {
        if(!attackInput)
        {
            attackInput = Input.GetKeyDown("j");
        }
    }

    void AttackFinished()
    {
        DisableHitbox();
        attacking = false;
        attackInput = false;
        animate.SetBool("Attacking", false);
        pHB.AttackFinished();
    }

    void ActivateHitbox()
    {
        hitBox.enabled = true;
    }

    void DisableHitbox()
    {
        hitBox.enabled = false;
    }
}
