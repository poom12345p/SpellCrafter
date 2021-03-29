using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ShowCaseItem : BaseUI
{
    [HideInInspector]
    public SItem itemData;
    [SerializeField]
    Image iconImage;
    [SerializeField]
    Text detailTextbox;

    void Update()
    {
        if (GetComponent<UIFade>().GetIsShow())
        {
            if (Input.GetButtonDown("A")) Close();
        }
    }

    public void Open(SItem sitem)
    {
        SetItemDetail(sitem);
        UIManager.instance.CallOpenUI(gameObject);
        //test
        //Invoke("CloseUI", 2.0f);
    }
    void SetItemDetail(SItem sitem)
    {
        itemData = sitem;
        iconImage.overrideSprite = itemData.image;
        detailTextbox.text = itemData.description;
    }

    public void Close()
    {
        UIManager.instance.CallCloseUI();
    }
}
