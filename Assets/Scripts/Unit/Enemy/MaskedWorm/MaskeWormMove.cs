using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskeWormMove : EnemyMove
{
    // Start is called before the first frame update
   
    [Space(20)]
    [Header("Masked Worm Parameter")]
    public float headUpTime;
    public Animator maskAnimator;
    public SpriteRenderer maskSprite;
    public DamageObject downDamage;

    static float attackCD = 2.0f;
    float attackCDcount=0.0f;
    bool headUp=false;
    public bool isMasked=true;

    void Start()
    {
        OnStart();
        isImmuneDaze = true;
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();   
    }

    public override void PrefromAttack()
    {
        if (attackCDcount <= 0&& !headUp)
        {
            // base.PrefromAttack();
            canMove = false;
            animator.SetBool("HeadUp", true);
            maskAnimator.SetBool("HeadUp", true);
            Invoke("HeadDownAttack", headUpTime);
            
            headUp = true;
        }
    }

    public  void HeadDownAttack()
    {
        // base.PrefromAttack();
        canMove = true;
        animator.SetBool("HeadUp", false);
        maskAnimator.SetBool("HeadUp", false);
        headUp = false;
        attackCDcount = attackCD;

    }

    public override void FaceTo(bool isLeft)
    {
        base.FaceTo(isLeft);
        //maskSprite.flipX = spriteRenderer.flipX;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (attackCDcount > 0 )
        {
            attackCDcount -= Time.deltaTime;
        }
    }

    public void DoDownDamage()
    {
        downDamage.ActiveHitOnce();
    }
}
