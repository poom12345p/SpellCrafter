using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer img;
    public TextMesh txt;
    public float fadeTime = 1f;

    float deltaFadeTime;
    bool act;

    void Start()
    {
        deltaFadeTime = 1 / fadeTime;
    }

    // Update is called once per frame
    void Update()
    {
        Fading(act);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) act = true;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player")) act = false;
    }

    /*public void Fading(bool act)
    {
        Color tmp = img.GetComponent<SpriteRenderer>().color;

        if (act) tmp.a += deltaFadeTime * Time.deltaTime;
        else tmp.a -= deltaFadeTime * Time.deltaTime;

        if (tmp.a < 0f) tmp.a = 0f;
        else if (tmp.a > 1f) tmp.a = 1f;
        else img.GetComponent<SpriteRenderer>().color = tmp;
    }*/

    public void Fading(bool act)
    {
        Color tmp = txt.GetComponent<TextMesh>().color;

        if (act) tmp.a += deltaFadeTime * Time.deltaTime;
        else tmp.a -= deltaFadeTime * Time.deltaTime;

        if (tmp.a < 0f) tmp.a = 0f;
        else if (tmp.a > 1f) tmp.a = 1f;
        else txt.GetComponent<TextMesh>().color = tmp;
    }
}
