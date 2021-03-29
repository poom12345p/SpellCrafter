using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTableUI : MonoBehaviour
{
    public Text ManaShardText,lifeShardText,potionShardText;
    Inventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTable();
    }

    public void UpdateTable()
    {
        playerInventory = GameManager.instance.gameSave.inventory;
        ManaShardText.text = playerInventory.manaShard + "/" + GameManager.manaShardUpgradeAmount;
        lifeShardText.text = playerInventory.lifeShard + "/" + GameManager.lifeShardUpgradeAmount;
        //potionShardText.text = playerInventory.potionShrad + "/" + GameManager.potionShardUpgradeAmount;
    }

    public void UpgradeMana()
    {
        if (playerInventory.manaShard >= GameManager.manaShardUpgradeAmount)
        {
            playerInventory.manaShard -= GameManager.manaShardUpgradeAmount;
            playerInventory.manaUp += 1;
            GameManager.instance.Player.SetInventory(playerInventory);
            GameManager.instance.Player.updateMana();
            GameManager.instance.SaveGame();
            UpdateTable();
        }
    }

    public void UpgradeLife()
    {
        if (playerInventory.lifeShard >= GameManager.lifeShardUpgradeAmount)
        {
            playerInventory.lifeShard -= GameManager.lifeShardUpgradeAmount;
            playerInventory.lifeUp += 1;
            GameManager.instance.Player.SetInventory(playerInventory);
            GameManager.instance.Player.updateLife();
            GameManager.instance.SaveGame();
            UpdateTable();
        }
    }

    public void UpgradePotion()
    {
        if (playerInventory.potionShrad >= GameManager.potionShardUpgradeAmount)
        {
            playerInventory.lifeShard -= GameManager.lifeShardUpgradeAmount;
            playerInventory.lifeUp += 1;
            GameManager.instance.Player.SetInventory(playerInventory);
            GameManager.instance.Player.updateLife();
            GameManager.instance.SaveGame();
            UpdateTable();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
