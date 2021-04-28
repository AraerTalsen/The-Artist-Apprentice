using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class DialogueBase : ScriptableObject
{

    [System.Serializable]
    public class Info
    {
        //public bool portraitToggle = true;
        //public string characterName;
        //public Sprite portrait;
        public AudioClip myVoice;
        //public Font myFont;
        [TextArea(4, 8)]
        public string myText;

        public UnityEvent myEvent;
    }

    [Header("Insert Dialogue Infornmation Below")]
    public Info[] dialogueInfo;
}
