using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSlimeMove : EnemyMove
{
    [SerializeField]
    protected float attackVelocity;
    //[SerializeField]
   // DamageObject DamageHitbox;

    [HideInInspector]
    public short atkStage=0;
    public bool isAtk;
    EnemyAI enemyAi;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        enemyAi = gameObject.GetComponent<EnemyAI>();
       // DamageHitbox.SetOwner(gameObject.GetComponent<Unit>());
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
        if(atkStage>0)
        {
            if (canMove) canMove = false;
        }
      
        if (atkStage ==2)
        {
          
            isImmuneDaze = true;
            if (enemyAi.nextIsGround)
            {
                rigid2D.velocity = new Vector2(attackVelocity * faceDirection*(slowTimeCount>0?0.5f:1.0f), rigid2D.velocity.y);
            }
            else
            {
                rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
            }
        }
        else if(atkStage ==3)
        {
            isImmuneDaze = false;
            isAtk = false;
            canMove = true;
            atkStage = 0;
        }
    }

    public override void PrefromAttack()
    {

        if (atkStage == 0)
        {
            isAtk = true;
            canMove = false;
            base.PrefromAttack();
       
            atkStage++;
       
        }
    
    }

    public void AtkStage(short s)
    {
        atkStage = s;
        //if(s==2)
        //{
        //    DamageHitbox.gameObject.SetActive(true);
        //}
    }


}
