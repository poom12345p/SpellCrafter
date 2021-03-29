using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharControl2 : MonoBehaviour
{
    Spellcraft_2 skm2;
    SpellUI spellUI;

    public LittleCasterMove MainCharMove;
    public GameObject skillManager, spellSpawn;

    bool isHoldJump = false;
    bool isHoldInter = false;
    bool isHoldItem = false;
    bool isActive;
    bool isSlow = false;
    // Start is called before the first frame update
    void Start()
    {
        skm2 = GetComponent<Spellcraft_2>();
        MainCharMove = GetComponent<LittleCasterMove>();
        spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            //MainCharMove.moveHorizontal(0);
            return;
        }
        //Debug.Log("Raw move axis " + Input.GetAxisRaw("Horizontal"));
        MainCharMove.moveHorizontal(Input.GetAxisRaw("Horizontal"));

        if (Input.GetAxisRaw("Jump") == 1)
        {
            if (!isHoldJump)
            {
                MainCharMove.Jump();
                isHoldJump = true;
                Debug.Log("jump");
            }
        }

        if (Input.GetAxisRaw("Jump") == 0 && isHoldJump)
        {
            MainCharMove.SetFalseJump();
            isHoldJump = false;
        }
        //if (isHoldJump)
        //{
        //    MainCharMove.HoldJump();
        //}

        // Debug.Log(Input.GetAxisRaw("Interact")+"Interact");
        if (Input.GetAxisRaw("Interaction") == 1 && !isHoldInter)
        {
            MainCharMove.Interact();
            isHoldInter = true;
        }
        else if(Input.GetAxisRaw("Interaction") == 0 && isHoldInter)
        {
            isHoldInter = false;
        }


        if (Input.GetAxisRaw("Item") == 1 && !isHoldItem)
        {
            MainCharMove.StartUsingPotion();
            isHoldItem = true;
        }
        else if (Input.GetAxisRaw("Item") == 0 && isHoldItem)
        {
            MainCharMove.EndUsingPotion();
            isHoldItem = false;
        }
        //SPELLCRAFT
        if (Input.GetAxisRaw("ShortcutNormal") == 1) skm2.SwitchElement(skm2.elementSet[0]);
        if (Input.GetAxisRaw("ShortcutFire") == 1) skm2.SwitchElement(skm2.elementSet[1]);
        if (Input.GetAxisRaw("ShortcutWater") == 1) skm2.SwitchElement(skm2.elementSet[3]);
        if (Input.GetAxisRaw("ShortcutWind") == 1) skm2.SwitchElement(skm2.elementSet[2]);
        if (Input.GetAxisRaw("ShortcutEarth") == 1) skm2.SwitchElement(skm2.elementSet[4]);

        //ATTACK
        if (Input.GetButtonDown("Fire1")) MainCharMove.StartAttack();
        if (Input.GetButtonUp("Fire1")) MainCharMove.LaunchAttack();

        //TOGGLE (HOLD)
        /*if (Input.GetAxisRaw("Fire2") == 1)
        {
            isToggle = true;
            spellUI.ChangeElement();
        }
        else isToggle = false;*/

        if (Input.GetButton("LB"))
        {
            isSlow = true;
            Time.timeScale = 0.05f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            spellUI.SetActiveSlow(true);
        }
        else
        {
            Time.timeScale = 1f;

            if (isSlow)
            {
                if (Input.GetAxisRaw("Alt Horizontal") == 0 && Input.GetAxisRaw("Alt Vertical") == 1) skm2.SwitchElement(skm2.elementSet[0]);
                if (Input.GetAxisRaw("Alt Horizontal") == -1 && Input.GetAxisRaw("Alt Vertical") == 0) skm2.SwitchElement(skm2.elementSet[1]);
                if (Input.GetAxisRaw("Alt Horizontal") == 1 && Input.GetAxisRaw("Alt Vertical") == 0) skm2.SwitchElement(skm2.elementSet[2]);
                if (Input.GetAxisRaw("Alt Horizontal") == -1 && Input.GetAxisRaw("Alt Vertical") == -1) skm2.SwitchElement(skm2.elementSet[3]);
                if (Input.GetAxisRaw("Alt Horizontal") == 1 && Input.GetAxisRaw("Alt Vertical") == -1) skm2.SwitchElement(skm2.elementSet[4]);
            }

            isSlow = false;
            spellUI.SetActiveSlow(false);
        }

        //CAST SPELL
        /*if (Input.GetAxisRaw("Fire2") == 1f)
        {
            MainCharMove.StartCast();
            MainCharMove.ParticleCast();
        }
        else if (Input.GetAxisRaw("Fire2") == 0f) MainCharMove.LaunchCast();*/

        /*if (Input.GetAxisRaw("Item01") == 0) MainCharMove.isUsePotion = false;
        else if (Input.GetAxisRaw("Item01") == -1 && !MainCharMove.isUsePotion)
        {
            MainCharMove.isUsePotion = true;
            MainCharMove.DrinkPotion();
        }*/

        /*if (Input.GetButtonDown("Shift"))
        {
            MainCharMove.SwapElementSlot();
        }*/
        //cm.MidairTime();
    }

    /*public void SetSpellUI(SpellUI _spellUI)
    {
        sui = _spellUI;
    }*/

    public void SetElement(Element ele)
    {
        if (ele == Element.FIRE) skm2.elementSet[0] = ele;
        else if (ele == Element.WIND) skm2.elementSet[1] = ele;
        else if (ele == Element.EARTH) skm2.elementSet[2] = ele;
        else if (ele == Element.WATER) skm2.elementSet[3] = ele;

        MainCharMove.SetElement(ele);
        spellUI.ChangeElement();
    }

    public void SetActiveControl(bool b)
    {
        MainCharMove.ClearAllAction();
        isActive = b;
        isHoldJump = false;
    }
}
