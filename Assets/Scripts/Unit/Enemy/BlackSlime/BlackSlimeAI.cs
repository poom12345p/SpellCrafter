using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSlimeAI :EnemyAI
{
    protected BlackSlimeMove blackSlimeMove;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        blackSlimeMove = (BlackSlimeMove)enemyMove;
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
        if(unit.isDaed&& blackSlimeMove.atkStage!=3)
        {
            blackSlimeMove.AtkStage(3);
        }
    }

}
