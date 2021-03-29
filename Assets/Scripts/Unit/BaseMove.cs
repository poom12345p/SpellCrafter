using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMove : MonoBehaviour, ReciveHit
{
    protected Rigidbody2D rigid2D;

    [Header("checkGorund")]
    public Vector3 checkGorundPoint;
    public float radius;
    public LayerMask groundLayer; 
    protected Collider2D standingGround;
    [Space(20)]

    protected bool isJump = false;

    public int faceDirection = 1;

    [Header("Animation")]
    public Animator animator;

    public SpriteRenderer spriteRenderer; 
    [Space(20)]

   

    [Header("Animation")]
    public float speedX = 10f;
    public float speedY = 10f;

    public bool canMove = true; 

    //[SerializeField]
    //protected float knockForce ;
    protected Vector3 knockBackDir;

    protected float knockbackTimeCount = 0.5f;
    public float KnockbackTime = 1f;
    protected bool isKnockback;

    //------------abnormal status--------------------
    public float slowTimeCount;
    [Header("Tranfromation")]
    public Transform[] fixFaceTranfrom;
    public Transform[] particleTransform;

    protected Unit unit;
    //protected IEnumerator Knockbacking(float speed, float time,Vector3 dir)
    //{
    //    if (!isKnockback)
    //    {
    //        isKnockback = true;
    //        while (time > 0)
    //        {
    //            yield return new WaitForFixedUpdate();
    //            time -= Time.fixedDeltaTime;
    //            rigid2D.velocity =( dir * speed);
    //            //rigid2D.AddForce(dir * speed);
    //        }

    //    }
    //}

    void Start()
    {
        OnStart();
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + checkGorundPoint, radius);
    }

    protected virtual void OnStart()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        var hitdetect = gameObject.GetComponent<BaseBody>();
        hitdetect.AddReciveHitObserver(GetComponent<ReciveHit>());
        unit = GetComponent<Unit>();
    }

    protected virtual void OnUpdate()
    {
        standingGround = Physics2D.OverlapCircle(transform.position + checkGorundPoint, radius, groundLayer);
      
        if (slowTimeCount > 0)
        {
            slowTimeCount -= Time.deltaTime;
        }

        if(isKnockback)
        {
            CheckKnockback();
        }
    }

    public void updateAnimatorValue(float xVelocity)
    {
        animator.SetFloat("xVelocity", xVelocity);
    }

    public virtual void Jump()
    {

        if (standingGround)
        {
            isJump = true;
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, speedY);
        }
    }

    public virtual void moveHorizontal(float horizotal)
    {
        if (!canMove || isKnockback)
        {
            //MAKE CHARACTER NOT BE MOVED BY INERTIA, ALSO SET ANIM. TO IDLE
            //rigid2D.velocity *= 0; don't use this code. it effect to knockback funtion.
            updateAnimatorValue(0);

            return;
        }

       //faceDirection = (int)horizotal;     

        float move = horizotal * speedX* getSpeedXMultiply();

        if (horizotal > 0)
        {
            FaceTo(false);
           // foreach (GameObject a in spellSpawn) a.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if (horizotal < 0)
        {
            FaceTo(true);
            //foreach (GameObject a in spellSpawn) a.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
        }
        if (rigid2D) rigid2D.velocity = new Vector2(move, rigid2D.velocity.y);
        //  rigid2D.velocity = Vector2.r *horizotal * speedX ;
        updateAnimatorValue(Mathf.Abs(horizotal));
    }

    public virtual void FaceTo(bool isLeft)
    {
        if (!canMove) return;

        if (isLeft)
        {
            // spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1,1,1);
            faceDirection = -1;
            foreach (var tf in fixFaceTranfrom)
            {
                tf.localScale = new Vector3(-1, 1, 1);
            }
            foreach (var pt in particleTransform)
            {
                pt.rotation = Quaternion.Euler(0, 0, -180);
                pt.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            // spriteRenderer.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
            faceDirection = 1;
            foreach (var tf in fixFaceTranfrom)
            {
                tf.localScale = new Vector3(1, 1, 1);
            }

            foreach (var pt in particleTransform)
            {
                pt.rotation = Quaternion.Euler(0, 0, 0);
                pt.localScale = new Vector3(1, 1, 1);
            }
        }
    }


    public virtual void PrefromReciveHit(HitArea hitArea)
    {
        //Debug.Log("checkKnockback");
        var dmgObj = (DamageObject)hitArea;
        var hitObj = hitArea.gameObject;
        if (dmgObj)
        {
            
            float knockForce = dmgObj.knockForce;
            knockBackDir.Set(transform.position.x - hitObj.transform.position.x, transform.position.y - hitObj.transform.position.y, 0);
            knockBackDir = knockBackDir.normalized;
           // rigid2D.AddForce(knockBackDir * knockForce);
            Knockback(knockForce, knockBackDir);
            //StartCoroutine(Knockbacking(20.0f, Time.fixedDeltaTime, knockBackDir));
            Debug.Log(gameObject.name + " knockBack ");
        }
    }

    public void CheckKnockback()
    {
        if (knockbackTimeCount <= 0) isKnockback = false;
        else
        {
            isKnockback = true;
            knockbackTimeCount -= Time.deltaTime;
        }

        //if (isKnockback)
        //{
        //    if (spriteRenderer.flipX) rigid2D.AddForce(new Vector2(knockForce, 0));
        //    else rigid2D.AddForce(new Vector2(-knockForce, 0));
        //}
    }

    public void Knockback(float KnockFoece,Vector3 dir)
    {
        isKnockback = true;
        knockbackTimeCount = KnockbackTime;
        dir = new Vector2(dir.x>0?1:-1,1f);
        // diry = new Vector3(0, dir., 0);
      if (rigid2D)  rigid2D.velocity = Vector2.zero;
      //  Debug.Log(rigid2D.velocity);
        if (rigid2D) rigid2D.AddForce(dir * KnockFoece, ForceMode2D.Impulse);
    }

    public void KnockbackSkill(float KnockFoece, Vector3 dir) 
    {
        isKnockback = true;
        knockbackTimeCount = KnockbackTime;
        //dir = new Vector2(dir.x > 0 ? 1 : -1, 1f);
        // diry = new Vector3(0, dir., 0);
        rigid2D.velocity = Vector2.zero;
        Debug.Log(rigid2D.velocity);
        rigid2D.AddForce(dir * KnockFoece, ForceMode2D.Impulse);
    }

    virtual protected float getSpeedXMultiply()
    {
        float mul = 1.0f; 
        if(unit&& unit.cold && unit.cold.IsStatusActive())
        {
            mul = 0.5f;
        }

       // mul = mul < 0.2f ? 0.2f : mul;
        return mul;
    }
     
    protected virtual void SetCanMove(bool b)
    {
        canMove = b;
    }


    public virtual void ReciveDamageAction()
    {
        //if (faceDirection < 0)
        //{
        //    foreach(var t in flipedWithDirObject)
        //    {
        //        t.rotation = Quaternion.Euler(0, 0, 180);
        //    }
        //} 
        //else
        //{
        //    foreach (var t in flipedWithDirObject)
        //    {
        //        t.rotation = Quaternion.Euler(0, 0, 0);
        //    }
        //}
    }

}
