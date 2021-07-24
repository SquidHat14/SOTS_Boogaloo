using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private void DESTROY_TEXT()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
