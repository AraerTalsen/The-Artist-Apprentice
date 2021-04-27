using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseFountainScript : MonoBehaviour
{
    public Player player;
    public GameObject overworldInteractableTrigger, triggerPointer;
    public SpriteRenderer fountainSpriteRenderer;
    public List<Sprite> fountainSpriteStates;

    void Update()
    {
        if (this.gameObject.activeSelf == true)
        {
            PlayerMovement.pauseGame = true;
        }
    }

    public void DrinkFromFountain()
    {
        switch (GlobalControl.numDrinksFromFountain)
        {
            case 0:
                player.currentHP = player.currentHP + 5;
                if (player.currentHP >= player.maxHP)
                    player.currentHP = player.maxHP;
                GlobalControl.numDrinksFromFountain = 1;
                fountainSpriteRenderer.sprite = fountainSpriteStates[1];
                PlayerMovement.pauseGame = false;
                this.gameObject.SetActive(false);
                break;
            case 1:
                player.currentHP = player.currentHP + 3;
                if (player.currentHP >= player.maxHP)
                    player.currentHP = player.maxHP;
                GlobalControl.numDrinksFromFountain = 2;
                fountainSpriteRenderer.sprite = fountainSpriteStates[2];
                PlayerMovement.pauseGame = false;
                this.gameObject.SetActive(false);
                break;
            case 2:
                player.currentHP = player.currentHP + 1;
                if (player.currentHP >= player.maxHP)
                    player.currentHP = player.maxHP;
                GlobalControl.numDrinksFromFountain = 3;
                fountainSpriteRenderer.sprite = fountainSpriteStates[3];
                PlayerMovement.pauseGame = false;
                this.gameObject.SetActive(false);
                break;
            case 3:
                player.currentHP = player.currentHP - 1;
                if (player.currentHP >= player.maxHP)
                    player.currentHP = player.maxHP;
                fountainSpriteRenderer.sprite = fountainSpriteStates[3];
                PlayerMovement.pauseGame = false;
                this.gameObject.SetActive(false);
                break;
            default:
                if (player.currentHP >= player.maxHP)
                    player.currentHP = player.maxHP;
                fountainSpriteRenderer.sprite = fountainSpriteStates[1];
                PlayerMovement.pauseGame = false;
                this.gameObject.SetActive(false);
                break;
        }
    }

    public void DoNotDrinkFromFountain()
    {
        PlayerMovement.pauseGame = false;
        this.gameObject.SetActive(false);
    }
}
