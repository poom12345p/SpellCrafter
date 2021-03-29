using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LittleCasterMove : BaseMove
{

    [Header("checkGorundHead")]
    public Vector3 checkHeadPoint;
    public float checkHeadradius;
    //public LayerMask groundLayer;
    protected Collider2D headGround;
    [Space(20)]

    [SerializeField]
    private float holdSpeedY;
    [SerializeField]
    private float jumpTime = 0.35f; 
    private float jumpTimeCounter;
    [SerializeField]
    private Vector3 lastJumpPos;
    private bool saveJumpPos;

    [SerializeField]
    private GameObject walkingPoint;
    public GameObject skillManager;

    public ParticleSystem LandingEffect;
    [SerializeField]
    public AudioSource landingAudio;
    public AudioSource lauchAudioSource;

    public AudioSource collectingAudioSource;

    private bool isFloating;
    [Space(20)]
    [Header("Charge Attack Parameter")]
    public float holdCastTime = 1.0f;
    public float clickAttackInterval = 0.25f, clickCastInterval = 0.25f, initialHold = 0.1f; //midairAttack = 0.5f, midairCast = 0.5f;
    public float[] holdAttackTime;
    bool isCharging=false;

    
    [Space(20)]
    public int maxJumpCount;
   // public ParticleSystem ps, swapElement;
    public bool isCast = false, isChannelling = false, respawning=false;

    float startHoldAttackTime = 0.0f, startHoldCastTime = 0.0f, startAttackClickInterval = 0.0f, startCastClickInterval = 0.0f, startMidairAttack = 0.0f, startMidairCast = 0.0f, knockForce = 20f;
    bool isReadyToCast = true, isReadyToAttack, isReadyDoubleJump, isMulJump, isQuickCast = true, isFallingDown = false;
    int jumpCount, potionCount;

    // [Space(20)]
    public GameObject noManaObj;
    ParticleSystem noManaParticle;
    AudioSource noManaAudio;

    [Header("Potion")]
    public ProcessBar potionProcess;
    public float usePotionTime =2.0f;
    float usePotionTimeCount=0.0f;
    bool isUsingPotion=false;
    public AudioSource potionAuduoSource;
    public AudioClip usingPotionSound;
    public AudioClip endUsingPotionSound;

    [SerializeField]
    Inventory inventory;

    [EnumNamedArray(typeof(Element))]
    public GameObject[] eleSpawnpoint;

    ManaSystem manaSystem
    {
        get
        {
            return GetComponent<ManaSystem>();
        }
    }
    Spell sp;
    MainCharControl2 mcc;
    Spellcraft_2 spc;
    MainUnit mainUnit;
    UIManager uiManager;

    // Start is called before the first frame update
    [HideInInspector]
    Interactable interectObj;

    public List<IAbility> eleSkill = new List<IAbility>();
    public IAbility currentSkill;

    public NoneElement noneElement;
    public FireElement fireElement;
    public WindElement windElement;
    public WaterElement waterElement;
    public EarthElement earthElement;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + checkGorundPoint, radius);
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + checkHeadPoint, checkHeadradius);
    }

    void Start()
    {
        OnStart();
        HideUsePotionBar();
    }

    IEnumerator WaitFadeRespawn()
    {
        yield return new WaitUntil(() => UIManager.instance.isFaded == true);
        UIManager.instance.FadeOut();
        ReposToRespawnPoint();
        yield return new WaitUntil(() => UIManager.instance.isFaded == false);

    }

    public void HideUsePotionBar()
    {
        potionProcess.transform.parent.gameObject.SetActive(false);
        potionProcess.gameObject.SetActive(false);
    }

    public void ShowUsePotionBar()
    {
        potionProcess.transform.parent.gameObject.SetActive(true);
        potionProcess.gameObject.SetActive(true);
    }


    protected override void OnStart()
    {
        base.OnStart();
        Transform staticObj = GameObject.FindGameObjectWithTag("Static").transform;
        LandingEffect = Instantiate(LandingEffect, staticObj);
        // manaSystem = GetComponent<ManaSystem>();
        sp = skillManager.GetComponent<Spell>();
        mcc = GetComponent<MainCharControl2>();
        spc = GetComponent<Spellcraft_2>();
        mainUnit = GetComponent<MainUnit>();
        //uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

        isReadyToAttack = isReadyDoubleJump = false;
        isMulJump = true;
        jumpCount = 0;
        //GetComponent<BaseBody>().AddReciveHitObserver(this);

        //Add Element
        noneElement = skillManager.GetComponent<NoneElement>();
        fireElement = skillManager.GetComponent<FireElement>();
        earthElement = skillManager.GetComponent<EarthElement>();
        waterElement = skillManager.GetComponent<WaterElement>();
        windElement = skillManager.GetComponent<WindElement>();

        eleSkill.Add(noneElement);
        eleSkill.Add(fireElement);
        eleSkill.Add(earthElement);
        eleSkill.Add(waterElement);
        eleSkill.Add(windElement);

        currentSkill = eleSkill[0];

        noManaParticle = noManaObj.GetComponent<ParticleSystem>();
        noManaAudio = noManaObj.GetComponent<AudioSource>();
    }

    public void SetUp()
    {
        OnStart();
    }
    // Update is called once per frame
    void Update()
    {
        OnUpdate();
       // moveHorizontal(0);
        //Debug.Log("play vx =" + rigid2D.velocity.magnitude);
        headGround = Physics2D.OverlapCircle(transform.position + checkHeadPoint, checkHeadradius, groundLayer);
        if (headGround && isJump)
        {
            SetFalseJump();
        }

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
        /*if (isFloating) //(isMidairAttack || isMidairCast))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
        else GetComponent<Rigidbody2D>().gravityScale = 1f;*/

        if (rigid2D.velocity.y < 0) walkingPoint.layer = 9;

        if (!standingGround)
        {
           if(isJump)
            {
                HoldJump();
            }
            isFloating = true;
            //Debug.Log(rigid2D.velocity.y);
            if (rigid2D.velocity.y < -20.0f)
            {
                rigid2D.velocity= new Vector2(rigid2D.velocity.x,-20.0f);
            }
        }

        if (isFloating && standingGround && walkingPoint.layer == 9)
        {
            Landing();
            isFloating = false;
            isMulJump = true;
            jumpCount = 0;
        }

        if(isUsingPotion)
        {
            
            usePotionTimeCount += Time.deltaTime;
            potionProcess.updateGauge(usePotionTime, usePotionTime-usePotionTimeCount);
            if(usePotionTimeCount>=usePotionTime)
            {
                isUsingPotion = false;
                EndUsingPotion();
                UsePotion();
            }
        }

        //moveHorizontal(Input.GetAxis("Horizontal"));

        //if (standingGround && standingGround.CompareTag("StableGround") && transform.position.x != lastJumpPos.x)
        //{
        //    lastJumpPos = new Vector3(transform.position.x - (faceDirection ), transform.position.y, 0);
        //}

        //MINOR-UPDATE
        //MidairTime();
        //SCheckKnockback();
        
        if (isReadyToAttack)
        {
            if(Time.time-startHoldAttackTime >= initialHold)
            {
                if (!isCharging)
                {
                    StartChargeAttack();
                }
                currentSkill.PreformCharging();
            }
        }

        //SUB-UPDATE
        //FallingDown();
    }

    public override void Jump()
    {
        if (!canMove || isKnockback || isUsingPotion) return;

        if ((standingGround && !isJump && canMove) || (jumpCount < maxJumpCount && !isJump && isMulJump && spc.currentElement == Element.WIND))
        {
           // Debug.Log("Jump");
            isJump = true;
            isMulJump = false;
            animator.SetBool("Jump", true);
            // rigid2D.AddForce(Vector2.up * speedY, ForceMode2D.Impulse);
            ///rigid2D.velocity = Vector2.up * speedY;
           // rigid2D.velocity = new Vector2(rigid2D.velocity.x, speedY);
            rigid2D.AddForce(Vector2.up * speedY,ForceMode2D.Impulse);
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
              //  Debug.Log("HoldJump");
                rigid2D.velocity = new Vector2(rigid2D.velocity.x, holdSpeedY);
                //rigid2D.AddForce(Vector2.up *holdSpeedY);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                SetFalseJump();
            }
        }
    }

    public void SetFalseJump()
    {
        if (isJump)
        {
            isJump = false;
            isMulJump = true;
            rigid2D.gravityScale = 1;
           // rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0);
        }
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
        if (respawning) return;
        GameManager.instance.SetLittleCasterControlActive(false);
        UIManager.instance.FadeIn();
        respawning = true;
        StartCoroutine("WaitFadeRespawn");

        //walkingPoint.layer = 10;
    }

    public void ReposToRespawnPoint()
    {
        GameManager.instance.SetLittleCasterControlActive(true);
        transform.position = lastJumpPos;
        respawning = false;
    }

    public void StartAttack()
    {
        if (isUsingPotion) return;
        startHoldAttackTime = Time.time;
        isReadyToAttack = true;
      
    }

    /*public void StartCast()
    {
        if (spc.currentElement != Element.NONE)
        {
            if (!isReadyToCast)
            {
                startHoldCastTime = startMidairCast = Time.time;
                isReadyToCast = true;
                canMove = false;
            }

            var goj = eleSpawnpoint[(int)spc.currentElement];
            if (goj != null && GetAbleToCast()) goj.SetActive(true);

            //QUICK CAST (CAST WHEN CLICK)
            if (isQuickCast)
            {
                bool isAbleToCast = true;
                if (goj != null && goj.GetComponent<CheckCollapse>() != null) isAbleToCast = goj.GetComponent<CheckCollapse>().isNotCollapse;

                if (isFloating)
                {
                    if (isAbleToCast)
                    {
                        /*sp.SingleCastSpell(spc.currentElement);
                        isFallingDown = true;
                        isQuickCast = false;
                        CancelInvoke();
                        Invoke("DelayFallingDown", 1f);
                    }
                }
            }
            else
            {
                startHoldCastTime = startCastClickInterval = Time.time;
                if (goj != null) goj.SetActive(false);
            }
        }
    }*/

    public void LaunchAttack()
    {
        isReadyToAttack = false;


        if (isUsingPotion || Time.time - startAttackClickInterval < clickAttackInterval)
        {
            isCharging = false;
            return;
        }

        
       bool isAbleToCast = true;

       // var goj = eleSpawnpoint[(int)spc.currentElement];
       // if (goj != null && goj.GetComponent<CheckCollapse>() != null) isAbleToCast = goj.GetComponent<CheckCollapse>().isNotCollapse;

        startAttackClickInterval = Time.time;
        if (!isCharging)
        {
           //startAttackClickInterval = Time.time;

            if (!isFloating) currentSkill.Attack();
            else currentSkill.MidairAttack();
        }
        else 
        {

            if (isAbleToCast)
            {
                if (!isFloating) currentSkill.Spell();
                else currentSkill.MidairSpell();
            }
            //canMove = true;
        }

        isCharging = false;
        canMove = true;

        //if (goj != null) goj.SetActive(false);

        //ps.Stop();

        //isJump = false => set false jump
    }

    /*public void LaunchCast()
    {
        if (isReadyToCast && spc.currentElement != Element.NONE)
        {
            bool isAbleToCast = true;
            var goj = eleSpawnpoint[(int)spc.currentElement];
            if (goj != null && goj.GetComponent<CheckCollapse>() != null) isAbleToCast = goj.GetComponent<CheckCollapse>().isNotCollapse;

            if (CheckHoldTime(holdCastTime, startHoldCastTime) && GetAbleToCast())
            {
                if (isAbleToCast) sp.SingleCastSpell(spc.currentElement);
                startCastClickInterval = Time.time;
            }
            else if (!CheckHoldTime(holdCastTime, startHoldCastTime))
            {
                if (isAbleToCast) sp.HoldCastSpell(spc.currentElement);
                isKnockback = true;
                KnockbackTime = Time.time;
            }

            if (goj != null) goj.SetActive(false);

            ps.Stop();
            isReadyToCast = false;
            canMove = isQuickCast = true;
            startHoldCastTime = startMidairCast = Time.time;
        }
    }*/

    /*public void MidairTime()
    {
        //if (Time.time >= startMidairAttack + midairAttack) isMidairAttack = false;
        if (Time.time >= startMidairCast + midairCast && isFloating) isUnableToMove = false;
    }*/
    
    /*public void UnMidAirCast()
    {
        isMidairCast = false;
    }*/

    public void StartChargeAttack()
    {
        //if (CheckHoldTime(holdAttackTime, startHoldAttackTime) == 1)
        //{
        //    currentSkill.PreformCharge();
      //  if (currentSkill.ActiveSpell())
       // {
            isCharging = true;
            currentSkill.PreformStartCharge();
        //    var goj = eleSpawnpoint[(int)spc.currentElement];
        //    if (goj != null && isCharging) goj.SetActive(true);
      //  }
           // ps.startColor = Color.cyan;
        //}
    }

    /*public void ParticleCast()
    {
        if (!CheckHoldTime(initialHold, startHoldCastTime) && spc.currentElement != Element.NONE)
        {
            if (!ps.isPlaying) ps.Play();
            ps.startColor = Color.green;
        }
        if (!CheckHoldTime(holdCastTime, startHoldCastTime) && spc.currentElement != Element.NONE)
        {
            if (!ps.isPlaying) ps.Play();
            ps.startColor = Color.magenta;
        }
    }*/

    public GameObject Cast(Spell.Skill skillData, float resizeX, float resizeY, float angle)
    {
        //ROTATION
        Quaternion q;
        q = Quaternion.Euler(0, 0, angle);       

        if (SpendMana(skillData))
        {
            GameObject skill = skillManager.GetComponent<ObjectPooler>().SpawnFromPool(skillData.tag, skillData.spawnPoint.transform.position, Quaternion.Euler(faceDirection == 1 ? 0 : 180, 0, faceDirection == 1 ? 0 : 180) * q);
            var dmgObj = skill.GetComponent<DamageObject>();
            //dmgObj.owner = gameObject.GetComponent<Unit>();
            Debug.Log(dmgObj);
            Debug.Log(gameObject.GetComponent<Unit>());
            dmgObj.SetOwner(gameObject.GetComponent<Unit>());

            skill.transform.localScale = new Vector3(resizeX, resizeY);
            //spc.currentElement = Element.NONE;

            lauchAudioSource.clip = skillData.lauchSoundClip;
            lauchAudioSource.Play();

            if (skillData.CastTime > 0)
            {
                animator.SetBool("Cast", true);
                Freeze();
                Invoke("UnCast", skillData.CastTime);
            }
            if (skillData.selfKnockForce > 0.0f)
            {
                KnockbackSkill(skillData.selfKnockForce, new Vector2(-faceDirection, 0.0f));
            }

            return skill;
        }
        else
        {
            noManaParticle.Play();
            noManaAudio.Play();
        }

        return null;
    }

    public void SpecialCast(Spell.Skill skillData, GameObject pos)
    {
        GameObject skill = skillManager.GetComponent<ObjectPooler>().SpawnFromPool(skillData.tag, pos.transform.position, pos.transform.rotation);
        var dmgObj = skill.GetComponent<DamageObject>();
        dmgObj.SetOwner(gameObject.GetComponent<Unit>());
    }

    public bool SpendMana(Spell.Skill skillInfo)
    {
        if (manaSystem.MP >= skillInfo.mp)
        {
            manaSystem.SpendMana(skillInfo.mp);
           
            return true;
        }
        return false;
    }

    public bool CheckSpellMana(Spell.Skill skillInfo)
    {
        if (manaSystem.MP >= skillInfo.mp) return true;
        return false;
    }

    /*public void SwapElementSlot()
    {
        Element temp;

        for (int i = 0; i < 3; i++)
        {
            temp = spc.elementSet[0 + i];
            spc.elementSet[0 + i] = spc.elementSet[3 + i];
            spc.elementSet[3 + i] = temp;
        }
        swapElement.Play();
    }*/

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


    public int CheckHoldTime(float[] ht, float sht)
    {
        float holdTime = Time.time - sht;
        for (int x = ht.Length - 1; x >= 0; x--)
        {
            if (holdTime >= ht[x]) return x + 1;
        }
        return 0;
    }

    public void RefilPotion()
    {
        potionCount = inventory.potion;
        uiManager.updatePotionText(potionCount);

    }
    public void StartUsingPotion()
    {
        if (potionCount > 0 && !isReadyToAttack && !isFloating)
        {
            isUsingPotion = true;
            usePotionTimeCount = 0.0f;
            ShowUsePotionBar();
            potionProcess.updateGaugeImediate(usePotionTime, usePotionTime);
            potionAuduoSource.clip = usingPotionSound;
            potionAuduoSource.loop = true;
            potionAuduoSource.Play();
        }
    }

    public void EndUsingPotion()
    {
        if (isUsingPotion)
        {
            isUsingPotion = false;
            //potionProcess.gameObject.SetActive(false);
            potionAuduoSource.Stop();
        }
        HideUsePotionBar();
        //usePotionTimeCount = 0.0f;
    }

    public void UsePotion()
    {
        if(potionCount > 0)
        {
            //inventory.potion -= 1;
            potionCount -= 1;
            mainUnit.Heal(inventory.GetPotionPower());
            uiManager.updatePotionText(potionCount);
            potionAuduoSource.Stop();
            potionAuduoSource.clip = endUsingPotionSound;
            potionAuduoSource.loop = false;
            potionAuduoSource.Play();
        }
    }

    public override void PrefromReciveHit(HitArea hitArea)
    {
        MapManager.instance.ShakeCam();
        base.PrefromReciveHit(hitArea);
        animator.SetTrigger("TakenDamage");
        //var Enemybody = hitArea.gameObject.GetComponent<EnemyUnit>();
        //if (Enemybody)
        //{
        //   // rigid2D.velocity.Set(0, rigid2D.velocity.y);
        //  //  knockBackDir.Set(transform.position.x - Enemybody.transform.position.x, transform.position.y - Enemybody.transform.position.y+10, 0);
        ////    knockBackDir = knockBackDir.normalized;
        // //   Knockback(800, knockBackDir);
        //  //  StartCoroutine(Knockbacking(20.0f, Time.fixedDeltaTime, knockBackDir));
        //}
        //Invoke("UnFreeze", FreezeeAfHitTime);

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
        if (interectObj && !isUsingPotion)
            interectObj.Interacted();
     
    }

    public void SetInventory(Inventory inv)
    {
        inventory = inv;
        var mainControl = GetComponent<Spellcraft_2>();


        earthElement.activeSpell = inv.canCastEarthBlock;

        //if (inventory.eleShard[0])
        //    mainControl.elementSet[0] = Element.FIRE;
        //if (inventory.eleShard[1])
        //    mainControl.elementSet[1] = Element.WIND;
        //if (inventory.eleShard[2])
        //    mainControl.elementSet[2] = Element.EARTH;
        //if (inventory.eleShard[3])
        //    mainControl.elementSet[3] = Element.WATER;
        //updatemp
       // manaSystem.updateMp(inventory);
        //updatelifePoint
        //update
    }

    public void ResetLittleCaster()
    {
        updateMana();
        updateLife();
        updatePotion();
    }

    public void updateMana()
    {
        manaSystem.updateMp(inventory);
    }

    public void updateLife()
    {
        mainUnit.UpdateUpgradedHp(inventory);
    }

    public void updatePotion()
    {
        potionCount = inventory.potion;
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

    public void CollectItem(ItemName name,int ea)
    {

        inventory.GetItem(name, ea);
        GameManager.instance.SaveGame();
        collectingAudioSource.Play();
    }

    public void SetMainCharSprite(GameObject charObj)
    {
        bool fx = spriteRenderer.flipX;
        animator = charObj.GetComponent<Animator>();
        spriteRenderer = charObj.GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = fx;
    }

    //public bool GetAbleToAttack()
    //{
    //    return Time.time >= startAttackClickInterval + clickAttackInterval;
    //}

    //public bool GetAbleToCast()
    //{
    //    return Time.time >= startCastClickInterval + clickCastInterval;
    //}

    public bool Floating()
    {
        return isFloating;
    }

    //SUB-ACTION

    public void MidairTime(float time)
    {
        canMove = false;
        CancelInvoke();
        Invoke("UnmidairTime", time);
    }

    public void UnmidairTime()
    {
        canMove = true;
    }


    public void Reborn()
    {
        mainUnit.isDaed = false;
        mainUnit.FullRestore();

    }



    public void SetReSpawnPos(Vector3 vector)
    {
        lastJumpPos = vector;
    }

    public void PrefromStay()
    {
        moveHorizontal(0);
        SetFalseJump();

    }

    /*public void DelayLaunchCast()
    {
        LaunchCast();
    }

    public void FallingDown()
    {
        if (isFallingDown)
        {
            canMove = false;
            GetComponent<AddForce>().enabled = true;
        }
        else GetComponent<AddForce>().enabled = false;
    }

    public void DelayFallingDown()
    {
        isFallingDown = false;
        canMove = true;
    }*/
    protected override float getSpeedXMultiply()
    {
        float mul = base.getSpeedXMultiply();
        if (isUsingPotion)
        {
            mul = 0.3f;
        }
        return mul;
         
    }

    public void ClearAllAction()
    {
        EndUsingPotion();
        currentSkill.CancelSpell();
    }


}
