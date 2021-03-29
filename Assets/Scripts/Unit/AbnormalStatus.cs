using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnormalStatus : MonoBehaviour
{
    
   // [SerializeField]
    float abnormalDamagetime =1.0f;
    float abnormalDamageCount = 0.0f;//*
   // [SerializeField]
   // float abnormalTime = 0.0f;
    float abmormalCounter = 0.0f;//*

    [SerializeField]
    ParticleSystem abnormalParticle;//*


    protected int damage;
    protected float damagePercent;

    bool isActive;

   protected Unit unit;
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
    public virtual void OnStart()
    {
        abnormalDamageCount = 0.0f;
        abmormalCounter = 0.0f;
        unit = gameObject.GetComponent<Unit>();
    }

    public virtual void OnUpdate()
    {
        if (abmormalCounter > 0 )
        {
            if (damagePercent > 0.0f || damage > 0.0f)
            {
                if (abnormalDamageCount <= 0.0f)
                {
                    DealDamage();
                    abnormalDamageCount = abnormalDamagetime;
                }
                else
                {
                    abnormalDamageCount -= Time.deltaTime;
                }
            }


            abmormalCounter -= Time.deltaTime;

            if (abmormalCounter <= 0)
            {
                EndStatus();
            }
        }

    }

    public virtual void HideStatusEffect()
    {
        if (abnormalParticle && abnormalParticle.isPlaying)
        {
            abnormalParticle.Stop(false);
            Debug.Log("Stop fire particle");
        }
    }

    public virtual void ShowStatusEffect()
    {
        if (abnormalParticle && !abnormalParticle.isPlaying)
        {
            abnormalParticle.Play();
        }
    }

    public virtual void DealDamage()
    {
        if (damagePercent > 0.0f)
        {
            float m = damagePercent;
            unit.TakkenDamage(unit.GetMaxHp() * m,Element.NONE);
        }

        if(damage > 0.0f)
        {
            unit.TakkenDamage(damage, Element.NONE);
        }
    }

    public virtual void StartStatus(float time)
    {
        if (abmormalCounter < time)
        {
            abmormalCounter = time;
            isActive = true;
            if (IsStatusActive())
            {
                ShowStatusEffect();
            }



        }
    
    }
    public virtual void EndStatus()
    {
        isActive = false;
        abmormalCounter = 0;
         HideStatusEffect();


    }

    public bool IsStatusActive()
    {

        return isActive;
    }
}
