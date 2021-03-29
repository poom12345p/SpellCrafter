using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    // Start is called before the first frame update
    [EnumNamedArray(typeof(Element))]
    public Sprite[] elements;
    public Image[] elementsSet;
    public Image currentElement, showCurrentElement;

    [HideInInspector]
    public GameObject character;
    [HideInInspector]
    public Spellcraft_2 skm;

    public GameObject slowUI;
    void Start() 
    {
        //if (character != null) skm = character.GetComponent<Spellcraft_2>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (character == null) GameObject.FindGameObjectWithTag("Player");

        /*int x = 0;
        foreach (Image a in elementsSet)
        {
            a.sprite = elements[(int)skm.elementSet[x]];
            x++;
        }
        currentElement.sprite = elements[(int)skm.currentElement];*/

        /*if (skm.isReadyCombine)
        {
            //timeCount.fillAmount = skm.timeCountLeft;
            currentElement.sprite = elements[(int)skm.CombineElement()];
        }
        else
        {
            timeCount.fillAmount = 0;
            currentElement.sprite = elements[(int)skm.currentElement];
            foreach (Image b in lineFlow) b.enabled = false;
        }*/

        /*int x = 0;
        foreach (Image a in elementsSet)
        {
            a.sprite = elements[(int)skm.elementSet[x]];
            x++;
        }
        currentElement.sprite = elements[(int)skm.currentElement];*/

        //Debug.Log(showCurrentElement);
    }

    /*public void SetSpellUI(int num) 
    {
        lineFlow[num].enabled = true;
    }*/

    public void ChangeElement()
    {
        //Debug.Log((int)skm.currentElement);

        /*int x = 0;
        foreach (Image a in elementsSet)
        {
            a.sprite = elements[(int)skm.elementSet[x]];
            x++;
        }*/
        currentElement.sprite = elements[(int)skm.currentElement];
        showCurrentElement.sprite = elements[(int)skm.currentElement];
    }

    public void SetItem()
    {
        
    }

    public void SetActiveSlow(bool boolean)
    {
        slowUI.SetActive(boolean);
    }
}
