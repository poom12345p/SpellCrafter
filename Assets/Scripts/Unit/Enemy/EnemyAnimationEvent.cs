using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent :AnimationEvent
{
    public EnemyAI enemyAI;
    public EnemyMove enemyMove;

    public void  disableSelf()
    {
        enemyMove.disableSelf();
    }

}
