using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Patrol: State
{
    EnemyStateMachine enemySM;

    AIMovement enemyAI;

    public Patrol(EnemyStateMachine esm)
    {
        enemySM = esm;
        enemyAI = enemySM.GetComponent<AIMovement>();
    }

    public override IEnumerator EnterState()
    {
        yield return new WaitForSeconds(1);
        enemyAI.setDirection(1);
        enemySM.startUpdate();
        yield break;
    }

    public override IEnumerator UpdateState()
    {   
        while(true)
        {
            if(enemyAI.controller.collisions.left || enemyAI.controller.collisions.right)
            {
                enemyAI.turnAround();
                yield return new WaitForSeconds(1);
            }
            yield return null;
        }
        yield break;
    }

    public override IEnumerator ExitState()
    {
        enemyAI.setDirection(0);
        yield break;
    }
}