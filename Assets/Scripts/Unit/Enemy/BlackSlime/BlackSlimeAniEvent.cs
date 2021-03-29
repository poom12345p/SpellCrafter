using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSlimeAniEvent : EnemyAnimationEvent
{
    protected BlackSlimeAI blackSlimeAI;
    protected BlackSlimeMove blackSlimeMove;
    // Start is called before the first frame update
    void Start()
    {
        blackSlimeAI = (BlackSlimeAI)enemyAI;
        blackSlimeMove = (BlackSlimeMove)enemyMove;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAtkStage(int s)
    {
        blackSlimeMove.AtkStage((short)s);
    }
}
