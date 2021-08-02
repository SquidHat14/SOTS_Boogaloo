  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine: MonoBehaviour
{
    protected State CurrentState;

    public void SetState(State state)
    {
        if(CurrentState != null) StartCoroutine(CurrentState.ExitState());

        CurrentState = state;
        StartCoroutine(CurrentState.EnterState());
    }

    public void startUpdate()
    {
        StartCoroutine(CurrentState.UpdateState());
    }
}