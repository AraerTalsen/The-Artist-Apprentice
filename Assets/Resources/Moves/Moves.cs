using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu]
public class Moves : ScriptableObject
{
    public int val;
    public int cost;
    public StatusEffect sf;
    public Entity[] targets;
    public bool isFriendlyTarget;
    public Sprite moveType;
    public Color effectColor;
    public GameObject miniGame;
}
