using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Run : Moves
{
    private CombatSystem cs;

    public void RunAway()
    {
        cs = FindObjectOfType<CombatSystem>();
        cs.LeaveBattle();
    }
}
