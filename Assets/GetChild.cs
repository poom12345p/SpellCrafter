using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetChild : MonoBehaviour
{
    // Start is called before the first frame update
    public bool setX, setY;
    bool isCheck;
    void OnEnable()
    {
        isCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<DamageObject>().owner != null && !isCheck)
        {
            for (int a = 0; a < transform.childCount; a++)
            {
                var child = transform.GetChild(a);
                child.gameObject.GetComponent<DamageObject>().owner = gameObject.GetComponent<DamageObject>().owner;
                child.gameObject.SetActive(true);
                if (setX && setY) child.position = transform.position;
                else if (setX) child.position = new Vector2(transform.position.x, child.transform.position.y);
                else if (setY) child.position = new Vector2(child.transform.position.x, transform.position.y);
            }
            isCheck = true;
        }
    }
}
