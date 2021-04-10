using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompletePanelScript : MonoBehaviour
{
    public string questCompleteOneText, questCompleteTwoText, questCompleteErrorCatch;

    public Text QuestCompleteDescription;

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.mostRecentQuestComplete == 1)
        {
            QuestCompleteDescription.text = questCompleteOneText;
        } else if (PlayerMovement.mostRecentQuestComplete == 2)
        {
            QuestCompleteDescription.text = questCompleteTwoText;
        } else if (PlayerMovement.mostRecentQuestComplete == 3)
        {
            QuestCompleteDescription.text = questCompleteOneText;
        }
        else
        {
            QuestCompleteDescription.text = questCompleteErrorCatch;
        }

        if (this.gameObject.activeSelf == true)
        {
            PlayerMovement.pauseGame = true;
        }
    }

    public void closeQuestCompleteMenu()
    {
        PlayerMovement.pauseGame = false;
        this.gameObject.SetActive(false);

    }
}
