using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : Interactable
{
    public SItem data;
    public int amount;
    //public void BeingCollect()
    //{

    //}

    public override void Interacted()
    {
        base.Interacted();
        player.CollectItem(data.itemName, amount);
        DisableInteract();
        UIManager.instance.OpenShowCaseItemUI(data);
        MapManager.instance.SaveMap();
    }
}
