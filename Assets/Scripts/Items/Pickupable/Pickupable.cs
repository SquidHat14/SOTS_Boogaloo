using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    
    protected virtual void PickupItem()
    {
        //Do some animation
        Destroy(this.gameObject);
    }
}
