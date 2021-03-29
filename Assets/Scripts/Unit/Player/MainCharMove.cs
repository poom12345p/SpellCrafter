using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCharMove : BaseMove
{
    [SerializeField]
    private float holdSpeedY;
    [SerializeField]
    private float jumpTime = 0.35f;
    private float jumpTimeCounter;
    [SerializeField]
    private Vector3 lastJumpPos;
    private bool saveJumpPos, isMidair = false;


    [SerializeField]
    private GameObject walkingPoint;
    public GameObject skillManager;

    public ParticleSystem LandingEffect;
    [SerializeField]
    public AudioSource landingAudio;

    

    private bool isFloating, isCasting;

    public float holdCastTime = 1.0f, clickAttackInterval = 0.25f, clickCastInterval = 0.25f, initialHold = 0.25f; //midairAttack = 0.5f, midairCast = 0.5f;
    public float FreezeeAfHitTime = 1.0f;
    public float[] holdAttackTime;
    public int maxJumpCount;
    public ParticleSystem ps, swapElement;
    public bool isMidairCast = false, isCast = false, isChannelling = false, isUsePotion = false;

    float startHoldAttackTime = 0.0f, startHoldCastTime = 0.0f, startAttackClickInterval = 0.0f, startCastClickInterval = 0.0f, startMidairAttack = 0.0f, startMidairCast = 0.0f;
    bool isReadyToCast, isReadyToAttack, isReadyDoubleJump, isMulJump;
    int jumpCount;
    [SerializeField]
    Inventory inventory;

    [EnumNamedArray(typeof(Element))]
    public GameObject[] eleSpawnpoint;

    ManaSystem ms;
    Spell sp;
    MainCharControl2 mcc;
    Spellcraft_2 spc;
    MainUnit mainUnit;
    UIManager uiManager;
    // Start is called before the first frame update
    [HideInInspector]
    Interactable interectObj;
    void Start()
    {
        OnStart();
        Transform staticObj = GameObject.FindGameObjectWithTag("Static").transform;
        LandingEffect = Instantiate(LandingEffect, staticObj);
        ms = GetComponent<ManaSystem>();
        sp = skillManager.GetComponent<Spell>();
        mcc = GetComponent<MainCharControl2>();
        spc = GetComponent<Spellcraft_2>();
        mainUnit = GetComponent<MainUnit>();
        //uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

        isReadyToAttack = isReadyDoubleJump = false;
        isMulJump = true;
        jumpCount = 0;
        //inventory = new Inventory();
        //uiManager.updatePotionText(inventory.potion);

    }

    // Update is called once per frame
    void Update()
    {

        OnUpdate();
       
        animator.SetFloat("yVelocity", rigid2D.velocity.y);
        //Debug.Log(rigid2D.velocity.y);
        animator.SetBool("ground",!isFloating);
        //Debug.Log(standingGround+"|"+walkingPoint.layer);

        //STEADY CASTING
        //if (mcc.isCast || (isFloating && (mcc.isMidairAttack || mcc.isMidairCast)))
        //{
        //    speedX = 0;
        //    canMove = false;
        //}
        //else
        //{
        //    speedX = 10;
        //    canMove = true;
        //}
        // rigid2D.velocity= new Vector3(200.0f * moveDirection, rigid2D.velocity.y,0);
        //MIDAIR FLOAT
        if (isFloating && isMidairCast)//(isMidairAttack || isMidairCast))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        else GetComponent<Rigidbody2D>().gravityScale = 1f;

        if (rigid2D.velocity.y < 0) walkingPoint.layer = 9;

        if (!standingGround) isFloating = true;
        if (isFloating && standingGround && walkingPoint.layer == 9)
        {
            Landing();
            isFloating = false;
            isMulJump = true;
            jumpCount = 0;
        }

        //moveHorizontal(Input.GetAxis("Horizontal"));

        //if (standingGround &&standingGround.CompareTag("StableGround") && standingGround.transform.position.x != lastJumpPos.x)
        //{
        //    lastJumpPos = new Vector3(standingGround.transform.position.x, transform.position.y+5, 0);
        //}

        //KNOCKBACK
        CheckKnockback();
        ParticleAttack();
        sp.ChannellingAttack(spc.currentElement);

        
    }

    public override void Jump()
    {
        if ((standingGround && !isJump && canMove) || (jumpCount < maxJumpCount && !isJump && isMulJump && spc.currentElement == Element.WIND))
        {
            Debug.Log("Jump");
            isJump = true;
            isMulJump = false;
            animator.SetBool("Jump", true);

             rigid2D.velocity = new Vector2(rigid2D.velocity.x, speedY);
             jumpTimeCounter = jumpTime;
            walkingPoint.layer = 10;
            jumpCount++;

            if (!isReadyDoubleJump) isReadyDoubleJump = true;

            rigid2D.gravityScale = 0;
        }

    }
    public void HoldJump()
    {
        if (isJump)
        {
            if (jumpTimeCounter > 0)
            {
                Debug.Log("HoldJump");
                 rigid2D.velocity = new Vector2(rigid2D.velocity.x, holdSpeedY);
                //rigid2D.AddForce(Vector2.up *holdSpeedY);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJump = false;
                rigid2D.gravityScale = 1;
            }
        }
    }

    public void SetFalseJump()
    {
        isJump = false;
        isMulJump = true;
        rigid2D.gravityScale = 1;
    }

    /*public void DoubleJump()
    {
        if (isDoubleJump)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3000);
            isDoubleJump = false;
        }
    }*/

    public void Respawn()
    {
        transform.position =  lastJumpPos;
        //walkingPoint.layer = 10;
    }

    public void StartAttack()
    {
        startHoldAttackTime = Time.time;
        isReadyToAttack = true;
    }

    public void StartCast()
    {
        if (!isReadyToCast) startHoldCastTime = Time.time;
        isReadyToCast = true;
    }

    public void LaunchAttack()
    {
        if (CheckHoldTime(holdAttackTime, startHoldAttackTime) == 0 && Time.time >= startAttackClickInterval + clickAttackInterval)
        {
            startAttackClickInterval = Time.time;
            sp.NormalAttack(spc.currentElement, 0);
        }
        else if (CheckHoldTime(holdAttackTime, startHoldAttackTime) == 1)
        {
            sp.NormalAttack(spc.currentElement, 1);
        }
        else if (CheckHoldTime(holdAttackTime, startHoldAttackTime) == 2)
        {
            sp.NormalAttack(spc.currentElement, 2);
        }
        ps.Stop();
        isReadyToAttack = isChannelling = false;
    }

    public void LaunchCast()
    {
        if (isReadyToCast && spc.currentElement != Element.NONE)
        {
            if (CheckHoldTime(holdCastTime, startHoldCastTime) && Time.time >= startCastClickInterval + clickCastInterval)
            {
                startCastClickInterval = startMidairCast = Time.time;
                sp.SingleCastSpell(spc.currentElement);
            }
            else if (!CheckHoldTime(holdCastTime, startHoldCastTime))
            {
                sp.HoldCastSpell(spc.currentElement);
                isKnockback = true;
                startMidairCast = KnockbackTime = Time.time;
            }

            var goj = eleSpawnpoint[(int)spc.currentElement];
            if (goj != null) goj.SetActive(false);

            ps.Stop();
            isReadyToCast = false;
            startHoldCastTime = Time.time;
        }
    }

    //public void MidairTime()
    //{
    //    if (Time.time >= startMidairAttack + midairAttack) isMidairAttack = false;
    //    if (Time.time >= startMidairCast + midairCast) isMidairCast = false;
    //}
    public void UnMidAirCast()
    {
        isMidairCast = false;
    }

    public void ParticleAttack()
    {
        if (!isReadyToAttack) startHoldAttackTime = Time.time;
        if (!CheckHoldTime(initialHold, startHoldAttackTime))
        {
            if (!ps.isPlaying) ps.Play();
            isChannelling = true;
            ps.startColor = Color.cyan;
        }
        if (CheckHoldTime(holdAttackTime, startHoldAttackTime) == 1)
        {
            if (!ps.isPlaying) ps.Play();
            ps.startColor = Color.yellow;
        }
        if (CheckHoldTime(holdAttackTime, startHoldAttackTime) == 2)
        {
            if (!ps.isPlaying) ps.Play();
            ps.startColor = Color.red;
        }
    }

    public void ParticleCast()
    {
        if (!CheckHoldTime(initialHold, startHoldCastTime) && spc.currentElement != Element.NONE)
        {
            var goj = eleSpawnpoint[(int)spc.currentElement];
            if (goj != null) goj.SetActive(true);

            if (!ps.isPlaying) ps.Play();
            ps.startColor = Color.green;
        }
        if (!CheckHoldTime(holdCastTime, startHoldCastTime) && spc.currentElement != Element.NONE)
        {
            if (!ps.isPlaying) ps.Play();
            ps.startColor = Color.magenta;
        }
    }

    public void Cast(Spell.Skill skillData, float resizeX, float resizeY, float angle)
    {
        //ROTATION
        Quaternion q;
        q = Quaternion.Euler(0, 0, angle);

        if (SpendMana(skillData))
        {
            GameObject skill = skillManager.GetComponent<ObjectPooler>().SpawnFromPool(skillData.tag, skillData.spawnPoint.transform.position, skillData.spawnPoint.transform.rotation * q);
            var dmgObj = skill.GetComponent<DamageObject>();
            //dmgObj.owner = gameObject.GetComponent<Unit>();
            dmgObj.SetOwner(gameObject.GetComponent<Unit>());

            skill.transform.localScale = new Vector3(resizeX, resizeY);
            //spc.currentElement = Element.NONE;
            if (skillData.CastTime > 0)
            {
                animator.SetBool("Cast", true);
                Freeze();
                Invoke("UnCast", skillData.CastTime);
            }
            if (skillData.knockback > 0.0f)
            {
                knockbackTimeCount = skillData.knockback;
               // knockForce = skillData.selfKnockForce;
                isKnockback = true;
            }
            if (isFloating&&!isMidairCast)
            {                
                isMidairCast = true;
                Invoke("UnMidAirCast", skillData.midAirFreezeTime);
            }
        }
    }

    public void SpecialCast(Spell.Skill skillData, GameObject pos)
    {
        GameObject skill = skillManager.GetComponent<ObjectPooler>().SpawnFromPool(skillData.tag, pos.transform.position, pos.transform.rotation);
        var dmgObj = skill.GetComponent<DamageObject>();
        dmgObj.SetOwner(gameObject.GetComponent<Unit>());
    }

    public bool SpendMana(Spell.Skill skillInfo)
    {
        if (ms.MP >= skillInfo.mp)
        {
            ms.MP -= skillInfo.mp;
            return true;
        }
        return false;
    }

    public void SwapElementSlot()
    {
        Element temp;

        for (int i = 0; i < 3; i++)
        {
            temp = spc.elementSet[0 + i];
            spc.elementSet[0 + i] = spc.elementSet[3 + i];
            spc.elementSet[3 + i] = temp;
        }
        swapElement.Play();
    }

    public void Landing()
    {
        LandingEffect.transform.position = walkingPoint.transform.position;
        landingAudio.Play();
        animator.SetBool("Jump", false);
        LandingEffect.Play();
    }

    protected void UnCast()
    {
        animator.SetBool("Cast", false);
        UnFreeze();
    }

    protected void Freeze()
    {
        rigid2D.velocity = Vector2.zero;
        rigid2D.isKinematic = true;
        SetCanMove(false);       
    }

    protected void UnFreeze()
    {
        SetCanMove(true);
        rigid2D.isKinematic = false;
    }

    public bool CheckHoldTime(float ht, float sht)
    {
        float holdTime = Time.time - sht;
        if (holdTime < ht) return true;
        return false;
    }

    public int CheckHoldTime(float[] ht, float sht)
    {
        float holdTime = Time.time - sht;
        for (int x = ht.Length - 1; x >= 0; x--) if (holdTime >= ht[x]) return x + 1;
        return 0;
    }

    public void DrinkPotion()
    {
        if(inventory.potion > 0)
        {
            inventory.potion -= 1;
            mainUnit.Heal(inventory.GetPotionPower());
            uiManager.updatePotionText(inventory.potion);
        }
    }

    public override void PrefromReciveHit(HitArea hitArea)
    {

        base.PrefromReciveHit(hitArea);
        Freeze();
        var Enemybody = hitArea.gameObject.GetComponent<EnemyUnit>();
        if (Enemybody)
        {
            rigid2D.velocity.Set(0, rigid2D.velocity.y);
            knockBackDir.Set(transform.position.x - Enemybody.transform.position.x, transform.position.y - Enemybody.transform.position.y, 0);
            knockBackDir = knockBackDir.normalized;
           // Knockback(800, knockBackDir);
            //  StartCoroutine(Knockbacking(20.0f, Time.fixedDeltaTime, knockBackDir));
        }
        Invoke("UnFreeze", FreezeeAfHitTime);

    }

    public void SetInteract(Interactable itr)
    {
        if(interectObj == null)
        {
            interectObj = itr;
        }
    }

    public void ClearInteract()
    {
        interectObj = null;
    }

    public void Interact()
    {
        if (interectObj)
            interectObj.Interacted();
     
    }

    public void SetInventory(Inventory inv)
    {
        inventory = inv;
        var mainControl = GetComponent<Spellcraft_2>();
        if (inventory.eleShard[0])
            mainControl.elementSet[0] = Element.FIRE;
        if (inventory.eleShard[1])
            mainControl.elementSet[1] = Element.WIND;
        if (inventory.eleShard[2])
            mainControl.elementSet[2] = Element.EARTH;
        if (inventory.eleShard[3])
            mainControl.elementSet[3] = Element.WATER;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public Interactable GetInteractable()
    {
        return interectObj;
    }

    public void SetUIManagr(UIManager ui)
    {
        uiManager = ui;
        uiManager.updatePotionText(inventory.potion);
    }

    public void SetElement(Element e)
    {
        if (e == Element.FIRE)
            inventory.eleShard[0] = true;

        if (e == Element.WIND)
            inventory.eleShard[1] = true;

        if (e == Element.EARTH)
            inventory.eleShard[2] = true;

        if (e == Element.WATER)
            inventory.eleShard[3] = true;

    }

    public void PickItem(ItemName name,int ea)
    {

        inventory.GetItem(name, ea);
    }

  
}
