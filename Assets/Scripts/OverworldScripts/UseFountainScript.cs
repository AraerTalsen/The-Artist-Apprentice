using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseFountainScript : MonoBehaviour
{
    public Player player;
    public GameObject overworldInteractableTrigger, triggerPointer;

    void Update()
    {
        if (this.gameObject.activeSelf == true)
        {
            PlayerMovement.pauseGame = true;
        }
    }

    public void DrinkFromFountain()
    {
        player.currentHP = player.maxHP;
        OverworldInteractableTrigger.fountainUsed = true;
        overworldInteractableTrigger.GetComponent<OverworldInteractableTrigger>().enabled = false;
        triggerPointer.SetActive(false);
        PlayerMovement.pauseGame = false;
        this.gameObject.SetActive(false);
    }

    public void DoNotDrinkFromFountain()
    {
        PlayerMovement.pauseGame = false;
        this.gameObject.SetActive(false);
    }
}
