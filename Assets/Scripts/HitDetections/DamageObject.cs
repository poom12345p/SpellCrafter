using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DamageObject : HitArea, PooledObject
{
    public Unit owner;

    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float damageMutiplyer;
    [SerializeField]
    protected Element myElement;
   
    public float knockForce;

    public bool doDaze = true;
    [SerializeField]
    bool isFixedDamage;
    [SerializeField]
    float slowTime;

    public float abnormalTime;


    public UnityEvent OnSpawnEvent;
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

        if(owner!=null)
        {
            SetOwner(owner);
        }
    }

    public void Setdamage(int dmg)
    {
        damage = dmg;
    
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetOwner(Unit user)
    {
        owner = user;
        if (!isFixedDamage)
        {
            if (damageMutiplyer != 0)
            {
                damage = (int)((float)owner.GetDamage() * damageMutiplyer);
            }
        }
    }

    public override void DoHitAction(GameObject hitObj)
    {
       
        base.DoHitAction(hitObj);
        var hitMove = hitObj.GetComponent<BaseMove>();
        var hitUnit = hitObj.GetComponent<Unit>();
        if(hitMove && slowTime>0)
        {
            hitMove.slowTimeCount = slowTime;
        }

        //if(hitUnit)
        //{
        //    if(abnormalTime>0)
        //    {
        //        hitUnit.SetAbnoemalStatus(abnormalTime, myElement);
        //    }

        //   // hitUnit.ReciveHitAction(gameObject);


        //}
    }
    public void SetActiveFalseWaitForSound()
    {
        isActiveHit = false;
        Invoke("Hide", doHitAudioClip.length);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void OnSpawn()
    {
        isActiveHit = true;
        OnSpawnEvent.Invoke();
    }

    public Element GetElement()
    {
        return myElement;
    }

    public override void Hit(GameObject hitObj)
    {
        var x = hitObj.GetComponent<BaseBody>();
        if (x == owner) return;
        base.Hit(hitObj);
    }
}
