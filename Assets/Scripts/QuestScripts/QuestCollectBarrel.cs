using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Barrel Quest", menuName = "Quests/Barrel Quest")]
public class QuestCollectBarrel : QuestBase
{
    [System.Serializable]
    public class Objectives
    {
        public QuestItemProfile barrel;
        public int requiredAmount;
    }

    public Objectives[] objectives;

    public override void InitializeQuest()
    {

        RequiredAmount = new int[objectives.Length];
        for (int i = 0; i < objectives.Length; i++)
        {
            RequiredAmount[i] = objectives[i].requiredAmount;
        }

        GlobalControl.Instance.onBarrelPickupCallBack += BarrelPickup;
        base.InitializeQuest();
    }

    private void BarrelPickup(QuestItemProfile pickedUpBarrel)
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            if(pickedUpBarrel == objectives[i].barrel)
            {
                CurrentAmount[i]++;
            }
        }

        Evaluate();
    }
}
