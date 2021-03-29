using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESCUI : MonoBehaviour
{
    /*public Text[] itemCountText;                    //FOR ESCUI
    public Text moneyText;
    public Text itemName, itemInfo, itemHold;       //FOR POPUP-ITEM-UI
    public Image item;
    public Sprite[] itemSet;
    public Text[] abiName, abiInfo;                 //FOR POPUP-ABI-UI
    public Image[] abi;
    public Sprite[] abiSet;
    public GameObject[] panel;

    bool openUI = false;
    int[] invValue = new int[12];
    int money;

    Inventory inv;
    Spellcraft_2 spcInfo;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inv = player.GetComponent<LittleCasterMove>().GetInventory();
        spcInfo = player.GetComponent<Spellcraft_2>();

        for (int i = 0; i < invValue.Length; i++) invValue[i] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openUI = !openUI;
            if (openUI)
            {
                panel[0].SetActive(true);
                panel[1].SetActive(true);
                panel[2].SetActive(false);
                panel[3].SetActive(false);
                panel[4].SetActive(false);
                panel[5].SetActive(false);
            }
            else panel[0].SetActive(false);
        }

        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        invValue[0] = inv.potionShrad;
        invValue[1] = inv.potion;
        invValue[2] = inv.lifeShard;
        invValue[3] = inv.lifeUp;
        invValue[4] = inv.manaShard;
        invValue[5] = inv.manaUp;
        invValue[6] = inv.magicBranch;
        invValue[7] = inv.weaponUpgrade;
        money = inv.money;

        for (int i = 0; i < itemCountText.Length; i++) if (itemCountText[i] != null) itemCountText[i].text = invValue[i].ToString();
        moneyText.text = "Money : " + money.ToString();
    }

    public void ShowItemPopup(int id)
    {
        panel[4].SetActive(true);

        string fullInfo = inv.itemInfo[id];
        string[] splits = fullInfo.Split('|');

        item.sprite = itemSet[id];

        itemName.text = splits[0];
        itemInfo.text = splits[1];
        itemHold.text = "Held : " + invValue[id]; 
    }

    public void ShowAbilityPopup(int id)
    {
        panel[5].SetActive(true);

        string[] fullInfo = spcInfo.elementInfo;
        string[] splits = fullInfo[id].Split('|');

        for (int i = 0; i < 3; i++)
        {
            abi[i].sprite = abiSet[(i * 4) + id];
            abiName[i].text = splits[(i * 2)];
            abiInfo[i].text = splits[(i * 2) + 1];
        }
    }

    public void SwapTab(int id)
    {
        for (int i = 1; i < panel.Length; i++)
        {
            if (i == id) panel[i].SetActive(true);
            else panel[i].SetActive(false);
        }
    }

    public void CloseItemPopup()
    {
        panel[4].SetActive(false);
    }

    public void CloseAbilityPopup()
    {
        panel[5].SetActive(false);
    }*/
}
