using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Unit : BaseBody, PooledObject
{
    
    public int baseDmage;

    [SerializeField]
    protected int BaseMaxHp;
    [HideInInspector]
    protected int maxHP;
    protected int HP;



    [SerializeField]
    protected List<Element> weakness;
    [SerializeField]
    protected List<Element> Strong;
    [SerializeField]
    protected List<Element> Absorb;

    //public int baseDmage;

   

    public UnityEvent afterDeadEvent;

    [SerializeField]
    protected ParticleSystem deadEffect;
    [SerializeField]
    protected AudioClip deadAudioClip;
    public bool isDaed = false;


    public UnityEvent OnSpawnEvent;
    [HideInInspector]
    public WetStatus wet;

    public BurnStatus burn;
    [HideInInspector]
    public ColdStatus cold;
    //bool isWet = false;
    //bool isOnFire = false;

   // float abnormalDamagetime = 0.5f, abnormalDamageCount = 0.0f;//*
    //float abnormalTime = 0.0f, abmormalCounter = 0.0f;//*

    [SerializeField]
    ParticleSystem onFireParticle;//*
    [SerializeField]
    ParticleSystem wetParticle;//*

    public LayerMask waterlayer;//*

    public BaseMove baseMove;


    public int GetDamage()
    {
        return baseDmage;
    }

    protected override void OnStart()
    {


        base.OnStart();
        maxHP = BaseMaxHp;
        HP = maxHP;
        if (deadEffect)
            deadEffect = Instantiate(deadEffect);

        baseMove = GetComponent<BaseMove>();
        wet = GetComponent<WetStatus>();
        burn = GetComponent<BurnStatus>();
        cold= GetComponent<ColdStatus>();
        //  weakness = new List<Element>();

        //  Strong = new List<Element>();

    }


    public override void OnUpdate()
    {

        //else
        //{
        //    isWet = false;
        //}
        base.OnUpdate();

        //if (isWet && IsWeakTo(Element.WATER))
        //{
        //    if (abnormalDamageCount <= 0.0f)
        //    {
        //        TakkenDamage(maxHP * 0.02f, null);
        //        abnormalDamageCount = abnormalDamagetime;
        //    }
        //    else
        //    {
        //        abnormalDamageCount -= Time.deltaTime;
        //    }
        //}

        //if (isOnFire)
        //{
        //    if (abnormalDamageCount <= 0.0f)
        //    {
        //        float m = 0.02f;
        //        if (IsWeakTo(Element.FIRE)) m = 0.04f;
        //        TakkenDamage(maxHP * m, null);
        //        abnormalDamageCount = abnormalDamagetime;
        //    }
        //    else
        //    {
        //        abnormalDamageCount -= Time.deltaTime;
        //    }
        //}




        if (isDaed)
        {
            isReciveDamage = false;
        }


        //if (isOnFire || isWet)
        //{
        //    abmormalCounter -= Time.deltaTime;
        //    if (abmormalCounter <= 0)
        //    {
        //        isOnFire = false;
        //        isWet = false;
        //        if (onFireParticle != null) onFireParticle.Stop();
        //        abnormalDamageCount = 0;
        //    }
        //}
    }

    //protected override void OnStart()
    //{
    //    base.OnStart();
    //    if (gameObject.layer != 15)
    //        gameObject.layer = 15;
    //}

    protected float getMutiply(Element e)
    {
        float mutiply = 1.0f;
        if (IsWeakTo(e)) mutiply *= 2.0f;
        else if (IsStrongTo(e)) mutiply *= 0.5f;

        if (wet && wet.IsStatusActive() && e == Element.ELECTRIC) mutiply *= 2.0f;

        return mutiply;
    }

    public override void ReciveHitAction(GameObject hitObj)
    {
        if (!isReciveDamage) return;
        base.ReciveHitAction(hitObj);
      
        var dmgObj = hitObj.GetComponent<DamageObject>();
        var unitObj = hitObj.GetComponent<Unit>();
        if (dmgObj || unitObj)
        {


            if (dmgObj)
            {

                SetAbnoemalStatus(dmgObj.abnormalTime, dmgObj.GetElement());
                TakkenDamage(dmgObj);
                //adjust weakness and storng damage


                //if (IsAbsorb(dmgObj.GetElement()))
                //{
                //    TakkenHeal(dmgObj.GetDamage());
                //}
                //else
                //{
                //    //adjust weakness and storng damage


                //    TakkenDamage(dmgObj.GetDamage() * getMutiply(dmgObj.GetElement()), dmgObj);
                //}

            }
            //if (unitObj)
            //{
            //    TakkenDamage(unitObj.baseDmage, dmgObj);
            //}

            disableDamagedCounter = disableDamagedTime;
            isReciveDamage = false;
            if (beingHitEffect)
            {
                beingHitEffect.transform.position = transform.position;
                beingHitEffect.Play();
            }
            //disableHitCounter = disableHitTime;
            //isActiveHit = false; 
            //base.ReciveHitAction(hitObj);
        }

    }




    //public void TakkenDamage(float dmg,Element elem, DamageObject dmgObj)
    //{
    //    //if(lossHPEffect!=null)
    //    //{
    //    //    lossHPEffect.transform.position = transform.position;
    //    //    lossHPEffect.transform.rotation = transform.rotation;
    //    //    lossHPEffect.Play();
    //    //}

    //    int rdmg = (int)Mathf.Round(dmg);
    //    TakkenDamage(rdmg, dmgObj);


    //}

    public void TakkenDamage(float dmg, Element elem)
    {
        //if(lossHPEffect!=null)
        //{
        //    lossHPEffect.transform.position = transform.position;
        //    lossHPEffect.transform.rotation = transform.rotation;
        //    lossHPEffect.Play();
        //}

        int rdmg = (int)Mathf.Round(dmg);
        TakkenDamage(rdmg, elem, null);


    }

    protected void TakkenDamage(DamageObject dmgObj)
    {
        //if(lossHPEffect!=null)
        //{
        //    lossHPEffect.transform.position = transform.position;
        //    lossHPEffect.transform.rotation = transform.rotation;
        //    lossHPEffect.Play();
        //}
        TakkenDamage(dmgObj.GetDamage(), dmgObj.GetElement(),dmgObj.owner);

        //HP -= dmg;
        //HP = HP < 0 ? 0 : HP;
        //if (HP == 0 && !isDaed) Dead(dmgObj);
        //if (baseMove) baseMove.ReciveDamageAction();
        //else
        //{
        //    Debug.LogWarningFormat("BaseMove of {0} is missing.", gameObject.name);
        //}
        //Debug.Log(gameObject.name + "being Hit HP:" + HP + "/" + maxHP + ":Hit by " + dmgObj.name);
    }

    protected virtual void TakkenDamage(int dmg,Element elem, Unit attacker)
    {

       // if (!isReciveDamage) return;
        if (IsAbsorb(elem))
        {
            TakkenHeal(dmg);
        }
        else
        {

            dmg = (int)(dmg *getMutiply(elem));
        }

        HP -= dmg;
        HP = HP < 0 ? 0 : HP;

        if (HP == 0 && !isDaed) Dead(attacker);
        if (baseMove) baseMove.ReciveDamageAction();
        else
        {
            Debug.LogWarningFormat("BaseMove of {0} is missing.", gameObject.name);
        }
        Debug.Log(gameObject.name + "being Hit HP:" + HP + "/" + maxHP);
    }


    protected void TakkenHeal(int h)
    {
        //if(lossHPEffect!=null)
        //{
        //    lossHPEffect.transform.position = transform.position;
        //    lossHPEffect.transform.rotation = transform.rotation;
        //    lossHPEffect.Play();
        //}


        HP += h;
        HP = Mathf.Clamp(HP, 0, maxHP);
        Debug.Log(gameObject.name + "+HP:" + HP + "/" + maxHP);
    }

    protected virtual void Dead(Unit attacker)
    {
        isDaed = true;
        // isDetectHit = false;
        if (deadEffect != null)
        {
            deadEffect.transform.position = transform.position;
            deadEffect.transform.rotation = transform.rotation;
            deadEffect.Play();
        }
        if (burn)
        {
            burn.EndStatus();
        }

        if (wet)
        {
            wet.EndStatus();
        }

        if (cold)
        {
            cold.EndStatus();
        }
        afterDeadEvent.Invoke();
    }

    public virtual void Dead()
    {
        Dead(null);

    }

    public virtual void DeActiveWaitForSound()
    {
        try
        {
            bodyAudioSource.clip = deadAudioClip;
            bodyAudioSource.Play();
        }
        catch
        {
            Debug.LogWarning("there are no audioSource for " + gameObject.name);
        }
        Invoke("DeActive", deadAudioClip.length);

    }

    public virtual void DeActive()
    {

        gameObject.SetActive(false);

    }

    public bool IsWeakTo(Element e)
    {
        if (weakness != null)
        {
            return weakness.Contains(e);
        }
        return false;

    }

    public void SetWeaknessElement(Element[] eles)
    {
        weakness.Clear();
        foreach (var e in eles)
        {
            weakness.Add(e);
        }
    }

    public bool IsStrongTo(Element e)
    {
        if (Strong != null)
        {
            return Strong.Contains(e);
        }
        return false;

    }

    public void SetStrongElement(Element[] eles)
    {
        Strong.Clear();
        foreach (var e in eles)
        {
            Strong.Add(e);
        }
    }

    public bool IsAbsorb(Element e)
    {
        if (Absorb != null)
        {
            return Absorb.Contains(e);
        }
        return false;

    }

    public void SetAbsorbElement(Element[] eles)
    {
        Absorb.Clear();
        foreach (var e in eles)
        {
            Absorb.Add(e);
        }
    }

    public void SetAbnoemalStatus(float time, Element e)
    {


       switch(e)
        {
            case Element.FIRE:
                if (wet)
                {
                    wet.EndStatus();
                }

                if (cold)
                {
                    cold.EndStatus();
                }
                if (burn)
                {
                    burn.StartStatus(time);
                }
                break;
            case Element.WATER:
               
                if (wet)
                {
                    if (burn && burn.IsStatusActive())
                    {
                        burn.EndStatus();
                    } 
                    else 
                    {
                        wet.StartStatus(time);
                    }
                }

                break;
            case Element.WIND:
                if (burn)
                {
                  if(burn.IsStatusActive())
                    {
                        burn.Swirl();
                    }
                }

                if (wet)
                {
                    Debug.LogFormat("{0} is wet", gameObject.name);
                    if (wet.IsStatusActive())
                    {
                        wet.EndStatus();
                        cold.StartStatus(time);
                    }
                }

                break;
               
        }
    }

    public int GetMaxHp()
    {
        return maxHP;
    }

    public void Reborn()
    {
        HP = maxHP;
        isDaed = false;
        isReciveDamage = true;
        disableDamagedCounter = 0;
    }

    public void OnSpawn()
    {
        Reborn();
        OnSpawnEvent.Invoke();
    }
}
