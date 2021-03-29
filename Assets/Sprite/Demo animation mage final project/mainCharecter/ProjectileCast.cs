using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCast : Cast
{
    // Start is called before the first frame update
    public float forceX, forceY;

    Rigidbody2D rig;
    GameObject character;
    BaseMove ch;

    void OnEnable()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        ch = character.GetComponent<BaseMove>();
        startSpawnTime = Time.time;
        rig = GetComponent<Rigidbody2D>();        

        if (ch.faceDirection == -1) rig.AddForce(new Vector2(-forceX, forceY), ForceMode2D.Impulse);
        else rig.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
    }

    /*void OnDisable()
    {
        rig.velocity = Vector3.zero;
        rig.angularVelocity = 0;
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startSpawnTime >= destroyedTime && haveDestroyedTime) gameObject.SetActive(false);
    }
}
