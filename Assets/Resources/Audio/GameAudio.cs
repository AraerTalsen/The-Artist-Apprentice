using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameAudio", menuName = "ScriptableObjects/GameAudio")]

public class GameAudio : SingletonScriptableObject<GameAudio>
{
    //FMODUnity.RuntimeManager.PlayOneShot(GameAudio.Instace.stringname);


    //Combat SFX
    public string Battle_Start;
    public string Debuff;
    public string Heal;
    public string Hit;
    public string Summon;
    public string Run;
    public string UI_Hover;
    public string UI_Select;

}
