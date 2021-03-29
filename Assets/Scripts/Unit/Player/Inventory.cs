using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public int potion = 3;
    public int potionPower = 20;
    public int potionShrad;
    public int lifeShard;
    public int lifeUp;
    public int manaShard; 
    public int manaUp;
    public int magicBranch;
    public int weaponUpgrade;
    public int money;

    [EnumNamedArray(typeof(Element))]
    public bool[] eleShard = new bool[10];

    public bool canCastEarthBlock;
    public Inventory() 
    {
        potion = 3;
        potionPower = 20;
        potionShrad = 0;
        lifeShard=0;
        lifeUp = 0;
        manaShard = 0;
        manaUp = 0;
        magicBranch = 0;
        weaponUpgrade = 0;
        money = 0;
        canCastEarthBlock = false;
        for (int i = 0; i < eleShard.Length; i++) eleShard[i] = false;
    }

    public int GetPotionPower()
    {
        return potionPower;
    }

    public bool[] GetElementShard()
    {
        return eleShard;
    }

    public void SetInventory(Inventory inv)
    {
        potion = inv.potion;
        potionPower = inv.potionPower;
        potionShrad = inv.potionShrad;
        lifeShard = inv.lifeShard;
        lifeUp = inv.lifeUp;
        manaShard = inv.manaShard;
        manaUp = inv.manaUp;
        magicBranch = inv.magicBranch;
        weaponUpgrade = inv.weaponUpgrade;
        money = inv.money;
        for (int i = 0; i < eleShard.Length; i++) eleShard[i] = inv.eleShard[i];
    }

    public void GetItem(ItemName name, int ea)
    {
        switch (name)
        {
            case ItemName.LifeShard:
                lifeShard += ea;
                break;
            case ItemName.ManaShard:
                manaShard+=ea;
                Debug.Log("Collect mana shard : " + ea + " | total : " + manaShard);
                break;
            case ItemName.Potion:
                potion += ea;
                break;
            case ItemName.PotinShard:
               potionShrad+= ea;
                break;
            case ItemName.TreeStick:
                magicBranch += ea;
                break;
        }
    }

}
