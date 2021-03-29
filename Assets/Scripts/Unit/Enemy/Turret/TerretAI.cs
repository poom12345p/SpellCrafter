using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerretAI : EnemyAI
{
    TurretMove turMove;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        turMove = (TurretMove)baseMove;
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    public override void OnUpdate()
    {
        //onPlayOffset.Set(offset.x * baseMove.faceDirection, offset.y, offset.z);
        //onGCPlayOffset.Set(GCoffset.x * baseMove.faceDirection, GCoffset.y, GCoffset.z);
        //nextIsGround = Physics2D.Linecast(transform.position + onGCPlayOffset, transform.position + onGCPlayOffset + Vector3.down, groundMask);
        //FindPlayer();
        if (!unit.isDaed)
        {
            if (TragetOnArea())
            {
                turMove.StartFire();
            }
            else
            {
                turMove.EndFire();
            }
        }
      
    }

    bool TragetOnArea()
    {
        
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position + onPlayOffset, viewBoxSize, 0);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != null && hitCollider.CompareTag("Player") && gameObject != hitCollider.gameObject)
            {
                return true;

            }

        }
        return false;
    }
}
