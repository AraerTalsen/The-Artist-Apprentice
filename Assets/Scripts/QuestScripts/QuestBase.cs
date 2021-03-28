using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase : ScriptableObject
{
    public string questName;
    [TextArea(5,10)]
    public string questDescription;

    public int[] CurrentAmount { get; set; }
    public int[] RequiredAmount { get; set; }

    public bool isCompleted { get; set; }
}
