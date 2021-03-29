using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : MonoBehaviour, IAbility
{
    LittleCasterMove lm;
    Spell sp;

    public ParticleSystem ps;
    public GameObject skill;
    [SerializeField]
    bool canCharge;
    public void Start()
    {
        lm = GameObject.FindGameObjectWithTag("Player").GetComponent<LittleCasterMove>();
        sp = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<Spell>();
    }
    public void PreformStartCharge()
    {
        if (canCharge)
        {
            skill.SetActive(true);
            if (!ps.isPlaying) ps.Play();
        }
    }
    public void PreformCharging()
    {
        
    }

    public void Attack()
    {
        lm.Cast(sp.skillDictionary["Pyroblast"], 0.5f, 0.5f, 0f);
        sp.GetIntervalSkill("Pyroblast");
    }
    public void Spell()
    {
        if (canCharge)
        {
            skill.SetActive(false);
            ps.Stop();
        }
    }

    public void QuickSpell()
    {
        
    }

    public void MidairAttack()
    {
        lm.Cast(sp.skillDictionary["Pyroblast"], 0.5f, 0.5f, 0f);
        sp.GetIntervalSkill("Pyroblast");
    }

    public void MidairSpell()
    {
        skill.SetActive(false);
        ps.Stop();
    }

    public bool ActiveSpell()
    {
        return false;
    }

    public void CancelSpell()
    {
    }
        /*public bool CheckChargeAttack()
        {
            return Input.GetButton("Fire1");           
        }*/
 }
