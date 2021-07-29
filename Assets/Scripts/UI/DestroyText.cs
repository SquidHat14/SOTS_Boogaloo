using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyText : MonoBehaviour
{
    private void DESTROY_TEXT()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
