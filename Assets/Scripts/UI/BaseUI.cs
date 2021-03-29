using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    UIFade uiFade;
    bool isOpen=false;
    // Start is called before the first frame update
    void Start()
    {
        //uiFade = GetComponent<UIFade>();
        OnStart();
    }

    public virtual void OnStart()
    {
        uiFade = GetComponent<UIFade>(); 
    }

    // Update is called once per frame

    public virtual void CloseUI()
    {
        if (!isOpen) return;

        if (uiFade) 
            uiFade.FadeOut();
        isOpen = false;
    }

    public virtual void OpenUI()
    {
        if (isOpen) return;

        if (uiFade)
            uiFade.FadeIn();
        isOpen = true;
    }
}
