using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUnit : Unit//, HitAble
{

    // Start is called before the first frame update

    public LittleCasterMove mainCharMove;

    public ProcessBar hpBar;

    protected ManaSystem manaSystem;

    static int hpPerUpgrade=20;

    bool canRespawn = true;
  
    void OnEnable()
    {
        OnStart();
        manaSystem = GetComponent<ManaSystem>();
        //hpBar = GameObject.Find("MainHpbar").GetComponent<ProcessBar>();
        //hpBar.updateGauge(maxHP, HP);
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
        
    }

  
    public override void ReciveHitAction(GameObject hitObj)
    {
        mainCharMove.ClearAllAction();
    

        base.ReciveHitAction(hitObj);

        if (hitObj.CompareTag("Spike") && canRespawn )
        {
            mainCharMove.Respawn();
            
        }
        //Debug.Log(hitObj.CompareTag("Spike"));

    }
    protected override void TakkenDamage(int dmg, Element elem, Unit attaker)
    {

       
        base.TakkenDamage(dmg,elem, attaker);
        hpBar.updateGauge(maxHP, HP);
    }

    public void Heal(int h)
    {
        TakkenHeal(h);
        hpBar.updateGaugeImediate(maxHP, HP);
    }

    protected override void Dead(Unit attaker)
    {
        base.Dead(attaker);
        canRespawn = false;
        //gameObject.SetActive(false);
        GameManager.instance.SetLittleCasterControlActive(false);
        Invoke("RebornPlayer", 0.1f);


    }

    public void RebornPlayer()
    {
        GameManager.instance.SaveGame();
        GameManager.instance.RebornPlayer();
        canRespawn = true;
    }

    public void SetHpbar(ProcessBar _hpbar)
    {
        hpBar = _hpbar;
        hpBar.updateGauge(maxHP, HP);
    }

    protected override void OnStart()
    {
        base.OnStart();
        mainCharMove = GetComponent<LittleCasterMove>();
        Transform staticObj = GameObject.FindGameObjectWithTag("Static").transform;
        if (deadEffect)
        {
            deadEffect.transform.SetParent(transform);
            deadEffect.transform.position = transform.position;
        }
        if (beingHitEffect)
        {
            beingHitEffect.transform.SetParent(transform);
            beingHitEffect.transform.position = transform.position;
        }
        //if (doHitEffect) doHitEffect.transform.SetParent(staticObj);
    }

   public void UpdateUpgradedHp(Inventory inv)
    {
        maxHP = BaseMaxHp + (hpPerUpgrade * inv.lifeUp);
        HP = maxHP;
       if(hpBar) hpBar.updateGaugeImediate(maxHP, HP);
    }

    public void FullRestore()
    {
        Heal(maxHP);
        manaSystem.FullMPRegen();
    }
}
