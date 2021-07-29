using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPickupable : Pickupable
{
    public HealingItem itemInfo;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 6)
        {
            StatsManager.Instance.Heal(itemInfo.healValue);
            UIManager.Instance.DisplayHealthPickup(transform.position, itemInfo.healValue);
            PickupItem();
        }
    }
}
