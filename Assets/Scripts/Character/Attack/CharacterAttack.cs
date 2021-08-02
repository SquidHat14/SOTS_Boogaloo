using UnityEngine;

public class CharacterAttack : MonoBehaviour
{    
    private bool attackInput = false;
    private Animator animate;
    public BoxCollider2D hitBox;
    private PlayerHitbox pHB;
    private float damage;

    public Weapon weapon;

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
        if(!attackInput)
        {
            attackInput = Input.GetKeyDown("j");
        }
    }

    public void StartFirstAttack()
    {
        updateHitBoxDimensions(weapon.attacks[0].hitboxOffset, weapon.attacks[0].hitboxSize);

        damage = weapon.attacks[0].damage;

        animate.Play(weapon.attacks[0].animationClipName);
        attackInput = false;
    }

     void AttackFinished(string animatorCondition)
    {
        DisableHitbox();
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

    void updateHitBoxDimensions(Vector2 offset, Vector2 size)
    {
        hitBox.size = size;
        hitBox.offset = offset;
    }

    public WeaponAttack getAttack()
    {
        return weapon.attacks[0];
    }
}
