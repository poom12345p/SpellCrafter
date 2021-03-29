using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : Interactable
{
    [SerializeField]
    ItemName itemName;
    [SerializeField]
    int amount;

    int Number;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interacted()
    {
        base.Interacted();
        player.CollectItem(itemName, amount);
    }
}
