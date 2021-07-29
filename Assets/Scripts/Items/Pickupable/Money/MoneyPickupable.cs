using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickupable : Pickupable
{
    public int moneyValue = 10;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 6)
        {
            InventoryManager.Instance.addMoney(moneyValue);
            UIManager.Instance.DisplayMoneyPickup(transform.position, moneyValue);
            PickupItem();
        }
    }

}
