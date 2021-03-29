using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RockSkill : DamageObject

{
    public UnityEvent DestoryRockEvent;

    // Start is called before the first frame update
    public void BeingDestory()
    {
        DestoryRockEvent.Invoke();
    }
    public override void Hit(GameObject hitObj)
    {
        base.Hit(hitObj);
        var unit = hitObj.GetComponent<BaseBody>();
        if (unit)
        {
            if(!unit.unBreakRock)
            {
                BeingDestory();
            }
        }
    }
}
