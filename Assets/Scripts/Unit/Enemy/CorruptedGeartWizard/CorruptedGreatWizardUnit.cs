using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedGreatWizardUnit : EnemyUnit
{
    [SerializeField]
    CorruptedGreatWizardAI corupptedGreatWizardAI;
    // Start is called before the first frame update
    void Start()
    {
        OnStart();
        corupptedGreatWizardAI = GetComponent<CorruptedGreatWizardAI>();


    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    protected override void TakkenDamage(int dmg, Element elem, Unit attacker)
    {
        base.TakkenDamage(dmg, elem, attacker);
        corupptedGreatWizardAI.GetHp(maxHP, HP);
    }
}
