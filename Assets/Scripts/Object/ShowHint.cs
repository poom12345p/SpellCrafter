using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHint : Interactable
{
    public SItem data;
    //public int amount;
    //public void BeingCollect()
    //{

    //}

    public override void Interacted()
    {
        //base.Interacted();
        //player.CollectItem(data.itemName, amount);
        //DisableInteract();
        UIManager.instance.OpenHintUI(data);
    }

    /*public void ShowHintUI(SItem data)
    {
        UIManager.instance.OpenHintUI(data);
    }*/
}
