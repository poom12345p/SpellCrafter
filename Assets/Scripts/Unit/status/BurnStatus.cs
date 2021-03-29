using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnStatus : AbnormalStatus
{


    [SerializeField]
    Material fireBodyMat;
    [SerializeField]
    DamageObject fireDamageArea;
    [SerializeField]
    DamageObject fireSwirlDamageArea;
    // Start is called before the first frame update
    float damageInterval = 0.5f;
    float damageIntervalCount =0.0f;
    void Start()
    {

        OnStart();
        damagePercent = 0.02f;
        HideStatusEffect();
        fireSwirlDamageArea = Instantiate(fireSwirlDamageArea.gameObject, transform.position, Quaternion.identity, transform).GetComponent<DamageObject>();
        //b.transform.localPosition = new Vector3(0.0f, b.transform.localScale.y / 2.0f, 0.0f);
    }
    private void Update()
    {
        OnUpdate();



        // Debug.LogFormat("{0},{1},{2},{3}", b.sprite.rect.x,b.sprite.rect.y, b.sprite.rect.width,b.sprite.rect.height);
    }
    // Update is called once per frame
    public override void DealDamage()
    {
        if (damagePercent > 0.0f)
        {
            float m = damagePercent;
            unit.TakkenDamage(unit.GetMaxHp() * m, Element.FIRE);
        }

        if (damage > 0.0f)
        {
            unit.TakkenDamage(damage, Element.FIRE);
        }
    }


    public override void StartStatus(float time)
    {
        base.StartStatus(time);
        damageIntervalCount =0;
        try
        {
            fireDamageArea.gameObject.SetActive(true);
        }
        catch
        {
            Debug.LogFormat("{0} :can't find fireDamageArea", gameObject.name);
        }
    }
    public override void EndStatus()
    {
        base.EndStatus();
        try
        {
            fireDamageArea.gameObject.SetActive(false);
        }
        catch
        {
            Debug.LogFormat("{0} :can't find fireDamageArea", gameObject.name);
        }
    }

    public void Swirl()
    {
        fireSwirlDamageArea.ActiveHitOnce();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (IsStatusActive())
        {
            damageIntervalCount += Time.deltaTime;
            if(damageIntervalCount>damageInterval)
            {
                damageIntervalCount = 0;
                if(fireDamageArea) fireDamageArea.ActiveHitOnce();
            }
        }
    }
}
