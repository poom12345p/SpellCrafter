using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskedWormAI : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    protected enum State
    {
        HEADUP, HEADDOWN
    }


  
}