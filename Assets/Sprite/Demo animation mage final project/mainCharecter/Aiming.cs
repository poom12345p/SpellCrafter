using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject aimPoint, character;
    BaseMove ch;
    void Start()
    {
        ch = character.GetComponent<BaseMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //REMOVE AIMING
        /*if (Mathf.Abs(Input.GetAxis("Alt Horizontal")) > 0.7f || Mathf.Abs(Input.GetAxis("Alt Vertical")) > 0.7f)
        {
            aimPoint.SetActive(true);
            transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(Input.GetAxis("Alt Horizontal"), Input.GetAxis("Alt Vertical")) * Mathf.Rad2Deg) - 90);
        }
        else
        {
            aimPoint.SetActive(false);
            if (ch.spriteRenderer.flipX) transform.rotation = Quaternion.Euler(0, 0, 180);
            else transform.rotation = Quaternion.Euler(0, 0, 0);
        }*/

        aimPoint.SetActive(true);
        var aimrt = aimPoint.GetComponent<RectTransform>();
        //aimPoint.GetComponent<RectTransform>().localScale.Set(ch.moveDirection, 0, 0);
        //if (ch.spriteRenderer.flipX) transform.rotation = Quaternion.Euler(0, 0, 180);
        //else transform.rotation = Quaternion.Euler(0, 0, 0);
       // transform.rotation = Quaternion.Euler(0, 0, ch.moveDirection>0?0:180);

        //Debug.Log((Mathf.Atan2(Input.GetAxis("Mouse X") - transform.position.x, Input.GetAxis("Mouse Y") - transform.position.y) * Mathf.Rad2Deg) - 90);

        /*aimPoint.SetActive(true);
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
    }
}
