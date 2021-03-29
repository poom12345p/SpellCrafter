using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseSetting : BaseUI
{
    // Start is called before the first frame update
    string[] Quality = { "Low", "Medium", "High"};
    string[] Mode = { "Fullscreen", "Windowed" };
    string[] Res = { "800|600", "1280|720", "1920|1080"};

    public Text qualityText, resText, modeText;
    public Slider sound;

    int qualityIndex = 2, resIndex = 2, modeIndex = 0;

    EventSystem es;
    bool isUpdate = false;

    void Start()
    {
        OnStart();
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = sound.value;

        if (GetComponent<UIFade>().GetIsShow())
        {
            if ((Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Horizontal") == 1) && !isUpdate)
            {
                UpdateSetting((int)Input.GetAxisRaw("Horizontal"));
            }
            else if (Input.GetAxisRaw("Horizontal") == 0) isUpdate = false;

            if (Input.GetButtonDown("A")) ApplyChange();
            if (Input.GetButtonDown("B"))
            {
                //UIManager.instance.CallCloseUI();
                UIManager.instance.isInGame = false;
                UIManager.instance.ReturnToMenu();
            }
        }
    }

    public void UpdateSetting(int value)
    {
        if (value == -1)
        {
            if (es.currentSelectedGameObject == qualityText.gameObject) QualityDown();
            else if (es.currentSelectedGameObject == resText.gameObject) ResDown();
            else if (es.currentSelectedGameObject == modeText.gameObject) ModeDown();
        }
        else if (value == 1)
        {
            if (es.currentSelectedGameObject == qualityText.gameObject) QualityUp();
            else if (es.currentSelectedGameObject == resText.gameObject) ResUp();
            else if (es.currentSelectedGameObject == modeText.gameObject) ModeUp();
        }
        isUpdate = true;
    }

    public void QualityUp()
    {
        if (qualityIndex < Quality.Length - 1) qualityIndex++;
        qualityText.text = Quality[qualityIndex];
    }
    public void QualityDown()
    {
        if (qualityIndex > 0) qualityIndex--;
        qualityText.text = Quality[qualityIndex];
    }

    public void ResUp()
    {
        if (resIndex < Res.Length - 1) resIndex++;
        string[] splits = Res[resIndex].Split('|');
        resText.text = splits[0] + " X " + splits[1];
    }
    public void ResDown()
    {
        if (resIndex > 0) resIndex--;
        string[] splits = Res[resIndex].Split('|');
        resText.text = splits[0] + " X " + splits[1];
    }

    public void ModeUp()
    {
        if (modeIndex < Mode.Length - 1) modeIndex++;
        modeText.text = Mode[modeIndex];
    }
    public void ModeDown()
    {
        if (modeIndex > 0) modeIndex--;
        modeText.text = Mode[modeIndex];
    }

    public void ApplyChange()
    {
        string[] splits = Res[resIndex].Split('|');
        Screen.SetResolution(int.Parse(splits[0]), int.Parse(splits[1]), modeIndex == 0 ? true : false);
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }
}
