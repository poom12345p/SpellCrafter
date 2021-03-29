using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollapse : MonoBehaviour
{
    public bool isNotCollapse = false;
    public Color notCollapse, collapse;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        isNotCollapse = IsEmpty();

        if (isNotCollapse) GetComponent<SpriteRenderer>().color = notCollapse;
        else GetComponent<SpriteRenderer>().color = collapse;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        count++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        count--;
    }

    public bool IsEmpty()
    {
        return count == 0;
    }
}
