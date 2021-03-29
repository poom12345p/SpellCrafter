using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : Interactable
{
    Animator anim
    {
        get
        {
            return GetComponent<Animator>();
        }
    }

 
    public void SetAnimActive()
    {
        anim.SetBool("Active",true);
        state = 1;
        //GameManager.instance.SaveGame();
        GetComponent<Collider2D>().enabled = false;
    }
    public void SetAnimUnActive()
    {
        anim.SetBool("Active", false);
        state = 0;
        // GameManager.instance.SaveGame();
        GetComponent<Collider2D>().enabled = true;
    }
    protected override void Checkstate()
    {
        base.Checkstate();
        if(state==0)
        {
            SetAnimUnActive();
        }
        else if(state == 1)
        {
        
            SetAnimActive();
        }
    }

}
