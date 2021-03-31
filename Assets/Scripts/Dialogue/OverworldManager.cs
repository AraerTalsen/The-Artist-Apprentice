using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class OverworldManager : MonoBehaviour
{
    public static OverworldManager instance;

    public List<GameObject> inkPortals;
    public List<GameObject> inkTiles;

    public Transform Player;

    public GameObject questBarrel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        if (PlayerMovement.barrelQuestItemGiven == true || PlayerMovement.barrelQuestItemPickedUp == true)
        {
            questBarrel.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inkPortals.Count; i++)
        {
            if (inkPortals[i].activeSelf == false)
            {
                inkTiles[i].SetActive(false);
            }
        }
    }
}
