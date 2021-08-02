using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine: StateMachine
{
    [HideInInspector]
    public GameObject player;


    //Enemy UI Stuff

    void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        SetState(new Patrol(this));
    }
}