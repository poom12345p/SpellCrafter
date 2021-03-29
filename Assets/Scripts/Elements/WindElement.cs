using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindElement : MonoBehaviour, IAbility
{
    LittleCasterMove lm;
    Spell sp;
    [SerializeField]
    bool canHoming;
    public void Start()
    {
        lm = GameObject.FindGameObjectWithTag("Player").GetComponent<LittleCasterMove>();
        sp = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<Spell>();
    }

    public void PreformStartCharge()
    {

    }
    public void PreformCharging()
    {
        
    }

    public void Attack()
    {
        var obj = lm.Cast(sp.skillDictionary["WindAttack"], 0.5f, 0.5f, 0f);
        if(!canHoming)
        {
            obj.GetComponent<TurnAround>().enabled = false;
        }
        sp.GetIntervalAttack("WindAttack");
    }
    public void Spell()
    {

    }

    public void QuickSpell()
    {
        
    }

    public void MidairAttack()
    {
        lm.Cast(sp.skillDictionary["WindAttack"], 0.5f, 0.5f, 0f);
        sp.GetIntervalAttack("WindAttack");
    }

    public void MidairSpell()
    {

    }

    public bool ActiveSpell()
    {
        return false;
    }
    public void CancelSpell()
    {
    }
}
