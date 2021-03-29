using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneElement : MonoBehaviour, IAbility
{
    LittleCasterMove lm;
    Spell sp;
    public float slamForce, slamTime;
    public ObjectPooler particlePool;
    public float[] holdAttackTime;
    public ParticleSystem[] chargeParticle;
    public ParticleSystem endChargeParticle;
    float holdTimeCount;
    bool finishCharge;
    public void Start()
    {
        lm = GameObject.FindGameObjectWithTag("Player").GetComponent<LittleCasterMove>();
        sp = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<Spell>();
        chargeParticle[0].gameObject.SetActive(false);
        endChargeParticle.gameObject.SetActive(false);
    }

    public void PreformStartCharge()
    {
        holdTimeCount = 0;
        chargeParticle[0].gameObject.SetActive(true);
        chargeParticle[0].Play();


    }
    public void PreformCharging()
    {
        holdTimeCount += Time.deltaTime;
        if(CheckHoldTime() > 0)
        {
            if (!finishCharge)
            {
                chargeParticle[0].GetComponent<AudioSource>().Stop();
                //chargeParticle[0].gameObject.SetActive(false);
                chargeParticle[0].Stop();

                endChargeParticle.gameObject.SetActive(true);
                endChargeParticle.Play();
                finishCharge = true;
            }
        }
    }

    public void Attack()
    {      
        var skillObj = lm.Cast(sp.skillDictionary["Normal Attack"], 1f, 1f, 0f);
        if (skillObj)
        {
            sp.GetIntervalAttack("Normal Attack");
            GameObject spellParticle = particlePool.SpawnFromPool("MagicCircleParticle", Quaternion.identity);
            spellParticle.GetComponent<ParticleSystem>().Play();
        }
        EndAtk();
    }
    public void Spell()
    {
        if (CheckHoldTime() > 0)
        {
           
            var skillObj =  lm.Cast(sp.skillDictionary["Heavy Attack"], 1f, 1f, 0f);
            if (skillObj)
            {
                sp.GetIntervalAttack("Heavy Attack");
                GameObject spellParticle = particlePool.SpawnFromPool("MagicCircleParticleBig", Quaternion.identity);
                spellParticle.GetComponent<ParticleSystem>().Play();
            }
            EndAtk();
            finishCharge = false;
        }
        else
        {
            Attack();
        }
    }


   public void EndAtk()
    {
        holdTimeCount = 0;
        foreach(var ps in chargeParticle)
        {
            ps.gameObject.SetActive(false);
            ps.Stop();
        }

        endChargeParticle.Stop();
        endChargeParticle.gameObject.SetActive(false);
       
    }

    public void CancelSpell()
    {
        EndAtk();
    }


    public void QuickSpell()
    {

    }

    public void MidairAttack()
    {
        Attack();
        //lm.Cast(sp.skillDictionary["Normal Attack"], 1f, 1f, 0f);
        //sp.GetIntervalAttack("Normal Attack");
    }

    public void MidairSpell()
    {
        Spell();
        //lm.Cast(sp.skillDictionary["Heavy Attack"], 1f, 1f, 0f);
        //sp.GetIntervalAttack("Heavy Attack");
    }
    public int CheckHoldTime()
    {

        for (int x = holdAttackTime.Length - 1; x >= 0; x--)
        {
            if (holdTimeCount >= holdAttackTime[x]) return x + 1;
        }
        return 0;
    }

    public bool ActiveSpell()
    {
        return false;
    }

}
