using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    LittleCasterMove mcm;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mcm = player.GetComponent<LittleCasterMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mcm.spriteRenderer.flipX) transform.rotation = Quaternion.Euler(0, 180, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
