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
            col.GetComponent<GeneralHurtbox>().GetHit(characterAttack.damage, collider.bounds.center.x);
        }

        hitCols.Add(col);
    }
}
