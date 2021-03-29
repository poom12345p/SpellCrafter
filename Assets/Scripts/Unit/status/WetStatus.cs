using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetStatus : AbnormalStatus
{
    // Start is called before the first frame update

    void Start()
    {
        OnStart();
        damagePercent = 0.02f;
    }

    private void Update()
    {
        OnUpdate();
    }
    // Update is called once per frame
    public override void DealDamage()
    {
        if (damagePercent > 0.0f)
        {

            if (unit.IsWeakTo(Element.WATER))
            {
                unit.TakkenDamage(unit.GetMaxHp() * damagePercent,Element.WATER);
            }
        }

    }
}
