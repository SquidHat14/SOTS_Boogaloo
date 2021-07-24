using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class EnemyHurtbox : GeneralHurtbox
{
    public bool HitPlayerOnTouch; //Is the collider also a hitbox?  If the player walks into the enemy, does it hurt the player?
    private EnemyStats stats; //Get health, damage, etc

    void Start()
    {
        base.Setup();
        stats = GetComponent<EnemyStats>();
    }

    protected override void TakeDamage(float damage, bool crit = false)
    {   
        stats.Health -= damage;

        base.TakeDamage(damage, crit);

        if (stats.Health <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        base.Die();  //(Maybe just call this in the death animation as a animation event)
    }
}
