using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Immobilizers : StatusEffect
{
    public override void Effect()
    {
        duration--;
        if (duration == 0) host.statusEffect[activationPeriod] = null;
        else
        {
            CombatSystem cs = FindObjectOfType<CombatSystem>();
            cs.state = 3;
            cs.StateMachine();
        }
    }
}
