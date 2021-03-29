using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : DamageObject
{
    // Start is called before the first frame update
    public float delayedTime = 0f;
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    public void DelayedDestroy()
    {
        gameObject.SetActive(false);
    }

    public override void DoHitAction(GameObject hitObj)
    {
        base.DoHitAction(hitObj);
        Invoke("DelayedDestroy", delayedTime);
    }


}
