using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRotate : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject character;
    public float range;

    BaseMove ch;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        ch = character.GetComponent<BaseMove>();
        InvokeRepeating("ChangeRotation", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRotation()
    {
        if (ch.spriteRenderer.flipX) transform.rotation = Quaternion.Euler(0, 0, 180 + Random.Range(-range, range));
        else transform.rotation = Quaternion.Euler(0, 0, 0 + Random.Range(-range, range));
    }
}
