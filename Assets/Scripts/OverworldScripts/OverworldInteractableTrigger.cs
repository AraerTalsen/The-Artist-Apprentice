using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldInteractableTrigger : Interactable
{
    public GameObject OverworldInteractableOptionsPanel;

    public override void Interact()
    {
            OverworldInteractableOptionsPanel.SetActive(true);

    }
}
