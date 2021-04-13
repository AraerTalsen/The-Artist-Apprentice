using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickedUp : MonoBehaviour
{
    public List<string> itemPickedUpText;

    public Text itemPickedUpDescription;

    public static int mostRecentItemPickedUp;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < itemPickedUpText.Count; i++)
        {
            if (i == mostRecentItemPickedUp)
            {
                itemPickedUpDescription.text=  itemPickedUpText[i];
            }
        }

        if (this.gameObject.activeSelf == true)
        {
            PlayerMovement.pauseGame = true;
        }
    }

    public void closeItemPickedUpMenu()
    {
        PlayerMovement.pauseGame = false;
        this.gameObject.SetActive(false);
    }
}
