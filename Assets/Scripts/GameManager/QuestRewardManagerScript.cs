using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRewardManagerScript : MonoBehaviour
{
    public GameObject sketchQuestRewardOne;
    public GameObject sketchQuestRewardTwo;
    public GameObject sketchQuestRewardThree;

    public static bool helpfulNPCReward = false;

    public static bool minion1Destroyed, minion2Destroyed, minion3Destroyed = false;


    // Update is called once per frame
    void Update()
    {
        if (minion1Destroyed == false)
        {
            if (PlayerMovement.barrelQuestItemGiven == true)
            {
                //sketchQuestRewardOne.SetActive(true);
            }
        }

        if (minion2Destroyed == false)
        {
            if (helpfulNPCReward == true)
            {
                //sketchQuestRewardTwo.SetActive(true);
            }
        }
 
    }
}
