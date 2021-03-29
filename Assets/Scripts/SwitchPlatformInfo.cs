using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPlatformInfo : MonoBehaviour
{
    // Start is called before the first frame update

    // [0] for PC, [1] for controller
    public Sprite[] icon;
    public string[] info; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string[] temp = Input.GetJoystickNames();

        if (temp.Length > 0)
        {
            if (!string.IsNullOrEmpty(temp[temp.Length - 1])) ChangeInfo(1);
            else ChangeInfo(0);
        }
    }

    public void ChangeInfo(int type)
    {
        if (GetComponent<SpriteRenderer>()) GetComponent<SpriteRenderer>().sprite = icon[type];
        else if (GetComponent<Image>()) GetComponent<Image>().sprite = icon[type];
        else if (GetComponent<TextMesh>()) GetComponent<TextMesh>().text = info[type].Replace("<space>", "\n");
    }
}
