using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public int activationPeriod;
    public int duration;
    public int val;
    public Entity host;

    public abstract void Effect();
}
