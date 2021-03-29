using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spellcraft_2 : MonoBehaviour
{
    float startCombineTime; 
    int MAX_ELEMENT_COUNT = 3;
    //HashSet<Element> elements = new HashSet<Element>();

    [EnumNamedArray(typeof(Element))]
    public ParticleSystem[] effPS;

    public Element currentElement;
    public Element[] elementSet = new Element[5];

    [EnumNamedArray(typeof(Element))]
    public GameObject[] CharSprites;
    public float combineTimeInterval, timeCountLeft;
    public bool isReadyCombine, changeSprite = false;

    LittleCasterMove littleCasterMove;
    SpellUI spellUI;
    Unit unit;

    [SerializeField]
    ParticleSystem swithParticle;
    // Start is called before the first frame update
    void Start()
    {
        littleCasterMove = GetComponent<LittleCasterMove>();
        spellUI = GameObject.Find("UIManager").GetComponent<SpellUI>();
        foreach (ParticleSystem ps in effPS) ps.Stop();
        currentElement = Element.NONE;
        unit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (temp != currentElement)
        {
            currentElement = temp;
            foreach (ParticleSystem ps in effPS) ps.Stop();
            effPS[(int)currentElement].Play();

            foreach(var charSprite in CharSprites)
            {
                if(charSprite)
                charSprite.SetActive(false);
            }
            CharSprites[(int)currentElement].SetActive(true);
            littleCasterMove.SetMainCharSprite(CharSprites[(int)currentElement]);

            switch (currentElement)
            {
                case Element.FIRE:
                    Element[] wf = { Element.WATER, Element.ICE };
                    Element[] sf = {  };

                    unit.SetWeaknessElement(wf);
                    unit.SetStrongElement(sf);
                    break;
                case Element.EARTH:
                    Element[] we = { Element.WATER, Element.LAVA };
                    Element[] se = { Element.ELECTRIC };
                    unit.SetWeaknessElement(we);
                    unit.SetStrongElement(se);
                    break;
                case Element.WIND:
                    Element[] ww = { Element.EARTH };
                    Element[] sw = { };
                    unit.SetWeaknessElement(ww);
                    unit.SetStrongElement(sw);
                    break;
                case Element.WATER:
                    Element[] wwt = { Element.EARTH, Element.GLASS ,Element.ELECTRIC};
                    Element[] swt = { };
                    unit.SetWeaknessElement(wwt);
                    unit.SetStrongElement(swt);
                    break;
                case Element.NONE:
                    Element[] wn = {};
                    Element[] sn = { };
                    unit.SetWeaknessElement(wn);
                    unit.SetStrongElement(sn);
                    break;
            }
        }*/

        //if (isReadyCombine) timeCountLeft = (combineTimeInterval - ((startCombineTime + combineTimeInterval) - Time.time)) * 2;
        //if (currentElement == Element.NONE) foreach (ParticleSystem ps in effPS) ps.Stop();
    }

    public void SwitchElement(Element ele)
    {
        if (currentElement != ele)
        {
            littleCasterMove.ClearAllAction();

            currentElement = ele;
            littleCasterMove.currentSkill = littleCasterMove.eleSkill[(int)currentElement];

            foreach (ParticleSystem ps in effPS) ps.Stop();
            effPS[(int)currentElement].Play();

            foreach (var charSprite in CharSprites)
            {
                if (charSprite) charSprite.SetActive(false);
            }
            CharSprites[(int)currentElement].SetActive(true);
            littleCasterMove.SetMainCharSprite(CharSprites[(int)currentElement]);
            swithParticle.Play();
            switch (currentElement)
            {
                case Element.NONE:
                    Element[] wn = { };
                    Element[] sn = { };
                    unit.SetWeaknessElement(wn);
                    unit.SetStrongElement(sn);
                    break;
                case Element.FIRE:
                    Element[] wf = { Element.WATER, Element.ICE };
                    Element[] sf = { };
                    unit.SetWeaknessElement(wf);
                    unit.SetStrongElement(sf);
                    break;
                case Element.EARTH:
                    Element[] we = { Element.WATER, Element.LAVA };
                    Element[] se = { Element.ELECTRIC };
                    unit.SetWeaknessElement(we);
                    unit.SetStrongElement(se);
                    break;
                case Element.WIND:
                    Element[] ww = { Element.EARTH };
                    Element[] sw = { };
                    unit.SetWeaknessElement(ww);
                    unit.SetStrongElement(sw);
                    break;
                case Element.WATER:
                    Element[] wwt = { Element.EARTH, Element.GLASS, Element.ELECTRIC };
                    Element[] swt = { };
                    unit.SetWeaknessElement(wwt);
                    unit.SetStrongElement(swt);
                    break;
            }

            if (currentElement == Element.NONE) foreach (ParticleSystem ps in effPS) ps.Stop();
            spellUI.ChangeElement();
        }
    }
}

    /*public Element CombineElement()
    {
        isReadyCombine = false;

        //ADD MORE COMBINE ELEMENTS
        return elements.First();
    }*/

    /*public Element CheckCombineElement()
    {
        //ADD MORE COMBINE ELEMENTS
        if (elements.Contains(Element.FIRE) && elements.Contains(Element.WIND) && elements.Contains(Element.EARTH)) return Element.GLASS;
        else if (elements.Contains(Element.FIRE) && elements.Contains(Element.WIND)) return Element.ELECTRIC;
        else if (elements.Contains(Element.FIRE) && elements.Contains(Element.EARTH)) return Element.LAVA;
        else if (elements.Contains(Element.WIND) && elements.Contains(Element.EARTH)) return Element.SAND;
        else return elements.First();
    }
}*/
