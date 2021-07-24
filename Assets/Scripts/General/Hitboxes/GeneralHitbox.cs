using System.Collections;
using UnityEngine;

public class GeneralHitbox : MonoBehaviour
{
    protected ArrayList hitCols;
    public LayerMask hittableLayers;
    protected BoxCollider2D collider;

    protected void Setup()
    {
        collider = GetComponent<BoxCollider2D>();
        hitCols = new ArrayList();
    }

    public void AttackFinished()
    {
        hitCols.Clear();
    }

    protected bool CheckIfHittable(Collider2D col)
    {
        /*
        Bitwise junk.  
        Layermasks are formed as a 32 bit integer where each bit represents a layer.
        Example - A Layermask that has layers 0,3,4,5 = 111001
        */

        return (hittableLayers == (hittableLayers | 1 << col.gameObject.layer) && !hitCols.Contains(col));  
    }

    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        if(CheckIfHittable(col))
        {
            col.GetComponent<GeneralHurtbox>().GetHit(0, collider.bounds.center.x);
        }

        hitCols.Add(col);
    }
}
