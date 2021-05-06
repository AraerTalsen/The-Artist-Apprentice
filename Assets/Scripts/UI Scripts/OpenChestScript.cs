using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenChestScript : MonoBehaviour
{
    public List<string> itemPickedUpText;
    public Text itemPickedUpDescription;
    public List<Sprite> chestStates;
    public SpriteRenderer chestSpriteRenderer;

    private void Start()
    {
        if (GlobalControl.chestKeyCollected == false && GlobalControl.chestOpened == false)
        {
            itemPickedUpDescription.text = itemPickedUpText[0];
            chestSpriteRenderer.sprite = chestStates[0];
        }
        else if (GlobalControl.chestKeyCollected == true && GlobalControl.chestOpened == false)
        {
            itemPickedUpDescription.text = itemPickedUpText[1];
            chestSpriteRenderer.sprite = chestStates[0];

        }
        else if (GlobalControl.chestOpened == true)
        {
            itemPickedUpDescription.text = itemPickedUpText[2];
            chestSpriteRenderer.sprite = chestStates[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == true)
        {
            PlayerMovement.pauseGame = true;
        }
    }

    public void OpenChestUIPanel()
    {
        if (GlobalControl.chestKeyCollected == false && GlobalControl.chestOpened == false)
        {
            itemPickedUpDescription.text = itemPickedUpText[3];
            chestSpriteRenderer.sprite = chestStates[0];
        }
        else if (GlobalControl.chestKeyCollected == true && GlobalControl.chestOpened == false)
        {
            itemPickedUpDescription.text = itemPickedUpText[4];
            chestSpriteRenderer.sprite = chestStates[1];
            GlobalControl.relicFiveCollected = true;
        }
        else if (GlobalControl.chestOpened == true)
        {
            itemPickedUpDescription.text = itemPickedUpText[2];
            chestSpriteRenderer.sprite = chestStates[1];
        }
    }

    public void CloseChestUIPanel()
    {
        PlayerMovement.pauseGame = false;
        this.gameObject.SetActive(false);
    }
}
