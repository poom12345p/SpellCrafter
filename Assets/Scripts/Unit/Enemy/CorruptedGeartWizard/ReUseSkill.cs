using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReUseSkill : MonoBehaviour,PooledObject
{
    public EnemyUnit enemyUnit;

    public void OnSpawn()
    {
        enemyUnit.Reborn();

    }
}
