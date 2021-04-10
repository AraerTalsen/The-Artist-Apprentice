using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldInteractableTrigger : Interactable
{
    public GameObject OverworldInteractableOptionsPanel;
    public static bool fountainUsed = false;

    public override void Interact()
    {
        if(fountainUsed == false)
        {
            Debug.Log("Interacted with Overworld Item");
            OverworldInteractableOptionsPanel.SetActive(true);
        } 
    }
}
