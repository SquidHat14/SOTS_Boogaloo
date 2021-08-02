using UnityEngine;

public class PlayerHitbox : GeneralHitbox
{
    private CharacterAttack characterAttack;
    void Start()
    {
        base.Setup();
        characterAttack = transform.parent.gameObject.GetComponent<CharacterAttack>();
    }
    protected override void OnTriggerStay2D(Collider2D col)
    {
        if(CheckIfHittable(col))
        {
            bool crit = Random.value * 100 <= characterAttack.weapon.critChance;
            WeaponAttack currentAttack = characterAttack.getAttack();
            col.GetComponent<GeneralHurtbox>().GetHit(currentAttack.damage * (crit == true ? characterAttack.weapon.critMultiplier : 1), collider.bounds.center.x, currentAttack.knockbackAngle, currentAttack.knockbackSpeed, crit);
        }
        hitCols.Add(col);
    }
}
