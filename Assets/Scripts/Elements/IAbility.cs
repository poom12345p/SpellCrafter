using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility 
{
    void PreformCharging();
    void PreformStartCharge();
    void Attack();
    void Spell();
    void CancelSpell();
    void QuickSpell();
    void MidairAttack();
    void MidairSpell();
    bool ActiveSpell();
}
