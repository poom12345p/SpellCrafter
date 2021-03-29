using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : BaseMove
{
    [SerializeField]
    protected float dazeTime;
    protected float dazeCount;
    protected bool isDaze;
    protected bool isImmuneDaze = false;
    float redTime;
    bool isRed;
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
    public virtual void PrefromAttack()
    {
        animator.SetTrigger("NormalAttack");

    }

    public void BeingDaze()
    { 
        if (!isImmuneDaze)
        {
            canMove = false;
            dazeCount = dazeTime;
            isDaze = true;
            try
            {
                animator.speed = 0;
                animator.SetTrigger("TakkenDmg");
            }
            catch
            {
                Debug.LogWarning(gameObject.name + " is,t have animator");
            }
        }
       // 
       
      
        ////
        redTime = dazeTime;
        isRed = true;
        ReverseColor();
    }

    void ShowDamagedColor()
    {
        var color = new Color(1, 0, 0, 1.0f);
        spriteRenderer.color = color;
    }

    public void ReverseColor()
    {
        Color color = new Color(1, 1, 1, 1.0f);
      
        if (unit && unit.cold && unit.cold.IsStatusActive())
        {
            color = new Color(0, 1, 1, 1.0f);
        }

        if (isRed)
        {
            color = new Color(1, 0, 0, 1.0f);
        }
        spriteRenderer.color = color;
    }

    protected override void OnUpdate()
    {

        base.OnUpdate();
        if (isDaze)
        {
            dazeCount -= Time.deltaTime;
        }

        if (dazeCount <= 0 && isDaze)
        {
             if(animator)  animator.speed = 1;
            canMove = true;
            isDaze = false;
            //ReverseColor();
        }

        if (redTime > 0)
        {
            redTime -= Time.deltaTime;
            if(redTime<=0)
            {
                isRed = false;
                ReverseColor();
            }
        }
      
    }

    public override void PrefromReciveHit(HitArea hitArea)
    {
        base.PrefromReciveHit(hitArea);
        var dmgObj = hitArea.gameObject.GetComponent<DamageObject>();
        if (dmgObj.doDaze && !isImmuneDaze)
        {
            BeingDaze();
        }
    }

    public void disableSelf()
    {
        gameObject.SetActive(false);
    }

    public void DoDeadAction()
    {
        animator.SetTrigger("Dead");
    }

    public override void ReciveDamageAction()
    {
        base.ReciveDamageAction();
        ///
        redTime = 0.1f;
        isRed = true;
        ShowDamagedColor();
        
    }

}
