using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public Image[] icon;
    int index = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetAxisRaw("Alt Horizontal") == -1) index = 0;
        else if (Input.GetAxisRaw("Alt Vertical") == 1) index = 1;
        else if (Input.GetAxisRaw("Alt Horizontal") == 1) index = 2;
        else if (Input.GetAxisRaw("Alt Vertical") == -1) index = 3;
        else index = 4;*/

        if (Input.GetAxisRaw("Alt Horizontal") == 0 && Input.GetAxisRaw("Alt Vertical") == 1) index = 0;            //NORMAL
        else if (Input.GetAxisRaw("Alt Horizontal") == -1 && Input.GetAxisRaw("Alt Vertical") == 0) index = 1;      //FIRE
        else if (Input.GetAxisRaw("Alt Horizontal") == 1 && Input.GetAxisRaw("Alt Vertical") == 0) index = 2;       //WIND
        else if (Input.GetAxisRaw("Alt Horizontal") == -1 && Input.GetAxisRaw("Alt Vertical") == -1) index = 3;     //WATER
        else if (Input.GetAxisRaw("Alt Horizontal") == 1 && Input.GetAxisRaw("Alt Vertical") == -1) index = 4;      //EARTH
        else index = 5;

        for (int i = 0; i < icon.Length; i++)
        {
            if (i == index) icon[i].color = new Color(1f, 1f, 1f, 1f);
            else icon[i].color = new Color(1f, 1f, 1f, 0f);
        }
    }


}
