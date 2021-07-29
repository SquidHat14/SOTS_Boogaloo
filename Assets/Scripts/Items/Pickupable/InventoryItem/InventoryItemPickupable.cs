using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemPickupable : Pickupable
{
    public InventoryItem itemInfo;
    
    private void OnTriggerEnter2D()
    {
        InventoryManager.Instance.addItemToInventory(itemInfo, 1);
        PickupItem();
    }
}
