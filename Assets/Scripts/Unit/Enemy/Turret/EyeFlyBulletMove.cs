using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFlyBulletMove : EnemyMove
{
    [SerializeField]
    LinearCast lc;
    float oriSpeed;
    // Start is called before the first frame update
    void Start()
    {
        lc = GetComponent<LinearCast>();
        oriSpeed = lc.speed;
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (oriSpeed == lc.speed)
        {
            if (unit && unit.cold && unit.cold.IsStatusActive())
            {
                lc.speed = oriSpeed / 2.0f;
            }
        }
        else
        {
            if (unit && unit.cold && !unit.cold.IsStatusActive())
            {
                lc.speed = oriSpeed ;
            }
        }
        OnUpdate();
    }
}
