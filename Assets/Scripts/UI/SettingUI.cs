using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingUI : BaseUI
{
    enum firstSelectedButton {Main, Abilities, Items, Upgrade};

    // Start is called before the first frame update
    public GameObject mainPanel;
    public GameObject[] subPanel;
    public float delayOpenPanel = 1f;

    GameObject player;
    EventSystem es;
    Inventory inv;
    float startDelayOpenPanel;
    bool isSubSettingOpen = false;

    //Ability Panel
    public SAbility currentAbilityInfo;
    public Text elementName, elementDescription, skillName, skillDescription;
    public Image attackIcon, spellIcon, midairIcon, passiveIcon, abilityImage;

    //Item Panel
    public SItem currentItemInfo;
    public Text itemName, itemDescription, itemCount;
    public Image itemImage;

    [EnumNamedArray(typeof(firstSelectedButton))]
    public Button[] firstSelected;

    GameObject currentSubUI;

    Animator bookAnim;
    public GameObject bookObj;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        inv = player.GetComponent<LittleCasterMove>().GetInventory();
        bookAnim = bookObj.GetComponent<Animator>();

        es.SetSelectedGameObject(null);
        currentSubUI = mainPanel;

        AbilityInfo(currentAbilityInfo);
        ItemInfo(currentItemInfo);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSubSettinguOpen)
            {
                foreach (GameObject g in subPanel) g.SetActive(false);
                isSubSettinguOpen = false;

                es.SetSelectedGameObject(firstSelected[(int)firstSelectedButton.Main].gameObject);
                firstSelected[(int)firstSelectedButton.Main].OnSelect(null);
            }
            else if (Time.time >= startDelayOpenPanel + delayOpenPanel)
            {
                mainPanel.SetActive(!mainPanel.activeSelf);
                if (mainPanel.activeSelf)
                {
                    es.SetSelectedGameObject(firstSelected[(int)firstSelectedButton.Main].gameObject);
                    firstSelected[(int)firstSelectedButton.Main].OnSelect(null);

                    //WRITE HERE FOR NOW
                    player.GetComponent<LittleCasterMove>().moveHorizontal(0);
                    player.GetComponent<LittleCasterMove>().canMove = false;
                }
                else player.GetComponent<LittleCasterMove>().canMove = true;
                GameManager.instance.SetLittleCasterControlActive(!mainPanel.activeSelf);

                startDelayOpenPanel = Time.time;
            }
        }*/

        if (Input.GetButtonDown("B") && (mainPanel.GetComponent<UIFade>().GetIsShow() || isSubSettingOpen)) OpenSetting();
        if (Input.GetButtonDown("Escape") && isSubSettingOpen) OpenSetting();
    }

    public void OpenSetting()
    {
        if (isSubSettingOpen)
        {
            bookAnim.Play("BookAnimReverse");
            UIManager.instance.CallCloseUI();
            UIManager.instance.CallOpenUI(mainPanel);
            isSubSettingOpen = false;
            GameManager.instance.SetLittleCasterControlActive(false);

            es.SetSelectedGameObject(firstSelected[(int)firstSelectedButton.Main].gameObject);
            firstSelected[(int)firstSelectedButton.Main].OnSelect(null);
        }
        else if (Time.time >= startDelayOpenPanel + delayOpenPanel)
        {
            currentSubUI = mainPanel;

            if (!mainPanel.GetComponent<UIFade>().GetIsShow())
            {
                es.SetSelectedGameObject(firstSelected[(int)firstSelectedButton.Main].gameObject);
                firstSelected[(int)firstSelectedButton.Main].OnSelect(null);
                UIManager.instance.CallOpenUI(currentSubUI);
            }
            else
            {
                UIManager.instance.CallCloseUI();
            }
            GameManager.instance.SetLittleCasterControlActive(!mainPanel.GetComponent<UIFade>().GetIsShow());

            startDelayOpenPanel = Time.time;
        }
    }

    public void OpenPanel(GameObject g)
    {
        if (mainPanel.GetComponent<UIFade>().GetIsShow())
        {
            if (g.name == "Abilities")
            {
                currentSubUI = subPanel[0];
                es.SetSelectedGameObject(firstSelected[(int)firstSelectedButton.Abilities].gameObject);
                firstSelected[(int)firstSelectedButton.Abilities].OnSelect(null);
            }

            else if (g.name == "Items")
            {
                currentSubUI = subPanel[1];
                es.SetSelectedGameObject(firstSelected[(int)firstSelectedButton.Items].gameObject);
                firstSelected[(int)firstSelectedButton.Items].OnSelect(null);
            }

            else if (g.name == "Upgrade")
            {
                currentSubUI = subPanel[2];
                es.SetSelectedGameObject(firstSelected[(int)firstSelectedButton.Upgrade].gameObject);
                firstSelected[(int)firstSelectedButton.Upgrade].OnSelect(null);
                UpgradeInfo();
            }

            //g.SetActive(true);
            bookObj.SetActive(true);
            bookAnim.Play("BookAnim");
            UIManager.instance.CallCloseUI();
            Invoke("DelayedOpenUI", 1f);
            isSubSettingOpen = true;
        }
    }

    //Ability Panel
    public void AbilityInfo(SAbility abi)
    {
        currentAbilityInfo = abi;

        elementName.text = currentAbilityInfo.elementName;
        elementDescription.text = currentAbilityInfo.elementDescription;
        skillName.text = currentAbilityInfo.attackName;
        skillDescription.text = currentAbilityInfo.attackDescription;

        attackIcon.sprite = currentAbilityInfo.attackIcon;
        spellIcon.sprite = currentAbilityInfo.spellIcon;
        midairIcon.sprite = currentAbilityInfo.midairIcon;
        passiveIcon.sprite = currentAbilityInfo.passiveIcon;
        abilityImage.sprite = currentAbilityInfo.attackClip;
    }

    public void AttackInfo()
    {
        skillName.text = currentAbilityInfo.attackName;
        skillDescription.text = currentAbilityInfo.attackDescription;
    }

    public void SpellInfo()
    {
        skillName.text = currentAbilityInfo.spellName;
        skillDescription.text = currentAbilityInfo.spellDescription;
    }

    public void MidairInfo()
    {
        skillName.text = currentAbilityInfo.midairName;
        skillDescription.text = currentAbilityInfo.midairDescription;
    }

    public void PassiveInfo()
    {
        skillName.text = currentAbilityInfo.passiveName;
        skillDescription.text = currentAbilityInfo.passiveDescription;
    }

    //Item Panel
    public void ItemInfo(SItem itm)
    {
        currentItemInfo = itm;

        itemName.text = itm.name;
        itemCount.text = "Held : " + inv.GetType().GetField(itm.hashcode).GetValue(inv);
        itemDescription.text = itm.description;
        itemImage.sprite = itm.image;
    }

    //Upgrade Panel
    public void UpgradeInfo()
    {
        subPanel[2].GetComponent<SaveTableUI>().UpdateTable();
    }

    //Sub-Function
    public void DelayedOpenUI()
    {
        UIManager.instance.CallOpenUI(currentSubUI);
    }
}
