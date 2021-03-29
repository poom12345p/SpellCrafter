using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIFade : MonoBehaviour
{
    [SerializeField]
    GameObject[] UIobj;
    List<Image> UIimg;
    List<Text> UIText;
    [SerializeField]
    float fadeTime;
    [SerializeField]
    bool isShow;
    bool isFading;
    float allAlpha;
    
    float deltaAlpha; 

    WaitForSecondsRealtime waitTime;
    [SerializeField]
    bool scaledTime;
    // Start is called before the first frame update
    void Start() 
    {
        waitTime = new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        UIimg = new List<Image>();
        UIText = new List<Text>();
        foreach (var ui in UIobj)
        {
            var img = ui.GetComponent<Image>();
            var txt = ui.GetComponent<Text>();
         
            if (img)
            {
                UIimg.Add(img);
            }
            else if (txt)
            {
                UIText.Add(txt);
            }
        }

        if (isShow)
        {
            allAlpha = 1.0f;
            SetAlpha(allAlpha);
        }
        else
        {
            allAlpha = 0.0f;
            SetAlpha(allAlpha);
        }

        SetRayCast(false);
        deltaAlpha = (1.0f/(1.0f/Time.unscaledDeltaTime)) / fadeTime;
        if(scaledTime)
        {
            deltaAlpha = Time.fixedDeltaTime / fadeTime;
        }
    }
    IEnumerator FadingUI()
    {

        if (!isFading)
        {
            isFading = true;
            //float timeCount = fadeTime;

            while ((allAlpha > 0 && !isShow) || (allAlpha < 1 && isShow))
            {
                //timeCount -= Time.unscaledDeltaTime;
                if (scaledTime)
                {
                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    yield return waitTime;
                }

                if (isShow)
                {
                    allAlpha += deltaAlpha;
                    SetRayCast(true);
                }
                else
                {
                    allAlpha -= deltaAlpha;
                    SetRayCast(false);
                }
                SetAlpha(allAlpha);
            }
            isFading = false;
        }
    }
    void SetAlpha(float a)
    {
        foreach (var img in UIimg)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
        }

        foreach (var txt in UIText)
        {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, a);
        }
    }

    void SetRayCast(bool val)
    {
        foreach (var img in UIimg)
        {
            img.raycastTarget = val;
        }

        foreach (var txt in UIText)
        {
            txt.raycastTarget = val;
        }
    }
  
    public void FadeIn()
    {
        if (isShow) return;

        isShow = true;
        StartCoroutine("FadingUI");
    }

    public void FadeOut()
    {
        if (!isShow) return;

        isShow = false;
        StartCoroutine("FadingUI");
    }

    public bool GetIsShow()
    {
        return isShow;
    }
}
