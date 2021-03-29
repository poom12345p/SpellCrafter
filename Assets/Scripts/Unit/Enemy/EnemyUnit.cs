using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    public int manaDropAmout;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }
    protected override void OnStart()
    {
        base.OnStart();
     
        if(gameObject.tag != "Enemy")
        {
            gameObject.tag = "Enemy";
        }
        baseMove = (EnemyMove)baseMove;
    }

    public override void ReciveHitAction(GameObject hitObj)
    {
        if (!isReciveDamage) return;
        base.ReciveHitAction(hitObj);

        var dmgObj = hitObj.GetComponent<DamageObject>();
        if (dmgObj && dmgObj.owner)
        {
            var manaSys = dmgObj.owner.GetComponent<ManaSystem>();
            var manaRe = hitObj.GetComponent<LCRegenManaDmg>();

            if (manaRe && manaSys)
            {

                //adjust weakness and storng damage
                float muti = 1.0f;
                if (IsWeakTo(dmgObj.GetElement()))
                {
                    muti = 2.0f;
                }
                var totalmp = (int)(manaRe.manaRegen * muti);
                //manaSys.gainMana(totalmp);
                manaSys.CreateManaDrop(totalmp, transform.position);
            }
        }
        Debug.Log(hitObj.CompareTag("Spike"));
        if (hitObj.CompareTag("Spike"))
        {
            TakkenDamage(HP,Element.NONE, this);
        }

    }
    protected override void Dead(Unit attacker)
    {
        ManaSystem manaSys = null;

        if (attacker)  manaSys = attacker.GetComponent<ManaSystem>();

        if(manaSys) manaSys.CreateManaDrop(manaDropAmout, transform.position);

        base.Dead(attacker);
    }

    


}
