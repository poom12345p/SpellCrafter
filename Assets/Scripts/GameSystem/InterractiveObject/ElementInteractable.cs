using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInteractable : Interactable
{
    public SItem data;
    public GameObject hint;

    public void GetElement(string e)
    {
        Element ele = (Element)System.Enum.Parse(typeof(Element), e);
        player.GetInventory().GetElementShard()[(int)ele] = true;
        //player.GetComponent<MainCharControl2>().SetElement(ele);        

        //SET ELEMENT SLOT FIRST TIME WHEN GET
        //if (ele == Element.EARTH) player.GetComponent<LittleCasterMove>().ee.activeSpell = true;
        //else if (ele == Element.WIND) player.GetComponent<MainCharControl2>().elementSet[1] = ele;
        //else if (ele == Element.EARTH) player.GetComponent<MainCharControl2>().elementSet[2] = ele;
        //else if (ele == Element.WATER) player.GetComponent<MainCharControl2>().elementSet[3] = ele;

        //ADD SCENE

        //CLEAR INTERACABLE
        Invoke("DisableInteract", 0.1f);
    }

    public void ActiveSpell(string e)
    {
        Element ele = (Element)System.Enum.Parse(typeof(Element), e);
        //GetComponent<ShowHint>().Interacted();

        //SpawnHint();

        if (ele == Element.EARTH)
        {
            player.GetComponent<LittleCasterMove>().earthElement.activeSpell = true;
            player.GetInventory().canCastEarthBlock = true;
            GameManager.instance.SaveGame();
        }
    }

    public void SpawnHint()
    {
        //hint.SetActive(true);
        GetComponent<ShowHint>().Interacted();
    }
}
