using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class State
{
    public virtual IEnumerator InitState(){yield break;}

    public virtual IEnumerator EnterState(){yield break;}

    public virtual IEnumerator UpdateState(){yield break;}

    public virtual IEnumerator ExitState(){yield break;}
}