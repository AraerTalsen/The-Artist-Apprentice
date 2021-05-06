using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PoisonDamager : StatusEffect
{
    public override void Effect()
    {
        host.currentHP -= val;
        CombatSystem cs = FindObjectOfType<CombatSystem>();
        cs.state = 1;
        cs.StateMachine();
        duration--;

        if (duration == 0)
        {
            host.statusEffect[activationPeriod] = null;
            Destroy(this);
        }
    }
}
