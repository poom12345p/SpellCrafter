using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuFirstSelect : MonoBehaviour
{
    // Start is called before the first frame update
    public Button firstSelected;
    GameObject es;
    
    void Start()
    {
        es = GameObject.Find("EventSystem");
        es.GetComponent<EventSystem>().SetSelectedGameObject(firstSelected.gameObject);
        firstSelected.OnSelect(null);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
