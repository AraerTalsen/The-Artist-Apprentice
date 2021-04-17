using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRelicUIWhileTalking : MonoBehaviour
{
    public GameObject relicUIGameObject;

    void Update()
    {
        if (DialogueManager.currentlyTalking == true)
        {
            relicUIGameObject.SetActive(false);
        }
        else
        {
            relicUIGameObject.SetActive(true);
        }
    }
}
