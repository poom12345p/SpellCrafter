using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElement : MonoBehaviour, IAbility
{
    LittleCasterMove lm;
    Spell sp;

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
        //Debug.Log("Water");
        lm.Cast(sp.skillDictionary["WaterAttack"], 1f, 1f, 0f);
        sp.GetIntervalAttack("WaterAttack");
    }
    public void Spell()
    {

    }

    public void QuickSpell()
    {
        
    }

    public void MidairAttack()
    {
        lm.Cast(sp.skillDictionary["WaterAttack"], 1f, 1f, 0f);
        sp.GetIntervalAttack("WaterAttack");
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
