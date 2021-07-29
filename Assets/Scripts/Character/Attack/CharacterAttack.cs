using UnityEngine;

public class CharacterAttack : MonoBehaviour
{    
    private bool attackInput = false;

    private bool acceptingAttackInput2 = false;
    private bool attackInput2 = false;
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
        if(attackInput)
        {
            StartFirstAttack();
        }
    }

    void Update()
    {
        if(!attackInput && !attackInput2)
        {
            attackInput = Input.GetKeyDown("j");
        }

        if(!attackInput && !attackInput2 && acceptingAttackInput2)
        {
            attackInput2 = Input.GetKeyDown("j");
        }
    }

    public void StartFirstAttack()
    {
        acceptingAttackInput2 = false;
        attackInput = false;
        animate.SetBool("Attacking", true);
    }

    public void LookForSecondAttackInput()
    {
        acceptingAttackInput2 = true;
    }

    public void StartSecondAttack()
    {
        if(!attackInput2) return;

        animate.SetBool("Attack2", true);
        //TO DO - Modify hitbox to make it larger and do more damage
        attackInput2 = false;
        acceptingAttackInput2 = false;
    }

     void AttackFinished(string animatorCondition)
    {
        DisableHitbox();
        animate.SetBool(animatorCondition, false);
        pHB.AttackFinished();  //Clears list of hit targets so 1 attack cant hit multiple times on the same enemy
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
