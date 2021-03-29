using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI : BaseUI
{
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OpenUI()
    {
        base.OpenUI();
        GameManager.instance.SetLittleCasterControlActive(false);
    }
    public void ReturnMainMenu()
    {
        base.CloseUI();
        UIManager.instance.ReturnToMenu();
    }
}
