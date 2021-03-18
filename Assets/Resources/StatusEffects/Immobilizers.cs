using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Immobilizers : StatusEffect
{
    public override void Effect()
    {
        CombatSystem cs = FindObjectOfType<CombatSystem>();
        cs.state = 3;
        cs.StateMachine();
        duration--;

        if (duration == 0)
        {
            host.statusEffect[activationPeriod] = null;
            Destroy(this);
        }  
    }
}
