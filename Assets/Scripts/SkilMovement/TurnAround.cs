using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject child, character;
    FindNearestTarget fnt;
    BaseMove ch;
    [SerializeField]
    float speed;
    void OnEnable()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        ch = character.GetComponent<BaseMove>();

        if (ch.spriteRenderer.flipX) transform.rotation = Quaternion.Euler(0, 0, 180);
        else transform.rotation = Quaternion.Euler(0, 0, 0);

        child = transform.GetChild(0).gameObject;
        fnt = child.GetComponent<FindNearestTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = fnt.currentHitOjb;

        if (target != null)
        {
            var relativePos = target.transform.position - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
        }
    }
}
