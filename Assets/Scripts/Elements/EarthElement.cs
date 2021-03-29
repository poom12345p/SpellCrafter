using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElement : MonoBehaviour, IAbility
{
    LittleCasterMove lm;
    Spell sp;

    public float slamForce, slamTime;
    public bool activeSpell = true, activeMidairSpell;
    public int spellCount = 3;
    public GameObject spawnPoint;

    Queue<GameObject> spellObject = new Queue<GameObject>(3);
    bool isActive;

    public ParticleSystem ps;


    public void Start()
    {
        lm = GameObject.FindGameObjectWithTag("Player").GetComponent<LittleCasterMove>();
        sp = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<Spell>();
    }

    public void Update()
    {
        //Slam();
        if (Input.GetButtonUp("Fire1")) spawnPoint.SetActive(false);
    }
    public void PreformStartCharge()
    {
        if (activeSpell && !lm.Floating())
        {
            spawnPoint.SetActive(true);
            lm.moveHorizontal(0);
            lm.canMove = false;
            lm.animator.SetBool("Cast", true);
            isActive = true;
        }
    }
    public void PreformCharging()
    {
      
    }

    public void Attack()
    {
        for (int i = 0; i < 3; i++) lm.Cast(sp.skillDictionary["EarthAttack"], 0.75f, 0.75f, (i - 1) * 10);
        sp.GetIntervalAttack("EarthAttack");
    }
    public void Spell()
    {
        if (activeSpell && isActive)
        {
            if (spawnPoint.GetComponent<CheckCollapse>().isNotCollapse && lm.CheckSpellMana(sp.skillDictionary["Rock"]))
            {
                GameObject g;

                if (spellCount > 0)
                {
                    spellCount--;
                }

                else
                {
                    var showPS = Instantiate(ps, spellObject.Peek().transform.position, spellObject.Peek().transform.rotation);
                    showPS.Play();
                    spellObject.Dequeue().SetActive(false);
                    //spellObject.Dequeue().GetComponent<RockSkill>().BeingDestory();
                }

                g = lm.Cast(sp.skillDictionary["Rock"], 1f, 1f, 0f);
                if (g != null) spellObject.Enqueue(g);
            }
        }

        spawnPoint.SetActive(false);
        lm.canMove = true;
        isActive = false;
        lm.animator.SetBool("Cast",false);
    }
    public void CancelSpell()
    {
        spawnPoint.SetActive(false);
        lm.canMove = true;
        isActive = false;
        lm.animator.SetBool("Cast", false);
    }
    public void QuickSpell()
    {
        
    }

    public void MidairAttack()
    {
        //OLD VERSION
        //lm.MidairTime(slamTime);

        for (int i = 0; i < 3; i++) lm.Cast(sp.skillDictionary["EarthAttack"], 0.75f, 0.75f, (i - 1) * 10);
        sp.GetIntervalAttack("EarthAttack");
    }

    public void MidairSpell()
    {

    }

    public bool ActiveSpell()
    {
        return activeSpell;
    }

    //SUB-FUNCTION
    public void Slam()
    {
        var rb2d = lm.GetComponent<Rigidbody2D>();

        if (lm.Floating() && !lm.canMove) rb2d.AddForce(Vector2.down * slamForce);
    }


}
