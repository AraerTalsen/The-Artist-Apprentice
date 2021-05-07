using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class OverworldManager : MonoBehaviour
{
    public static OverworldManager instance;

    public List<GameObject> inkPortals;
    public List<GameObject> inkTiles;
    public List<GameObject> lootRewards;
    public List<GameObject> overWorldEnemies;
    public List<GameObject> caveDoorBlockers;
    public List<GameObject> caveEntraceColliders;

    public Transform Player;

    public GameObject questBarrel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inkPortals.Count; i++)
        {
            if (inkPortals[i].activeSelf == false)
            {
                //inkTiles[i].SetActive(false);

                //I am so sorry it's so ugly but brain is mush
                if(GlobalControl.relicOneCollected == false && inkPortals[0].activeSelf == false)
                {
                    lootRewards[0].SetActive(true);
                }
                if (GlobalControl.relicTwoCollected == false && inkPortals[1].activeSelf == false)
                {
                    lootRewards[1].SetActive(true);
                }
                if (GlobalControl.relicThreeCollected == false && inkPortals[2].activeSelf == false)
                {
                    lootRewards[2].SetActive(true);
                }
                if (GlobalControl.relicFourCollected == false && inkPortals[3].activeSelf == false)
                {
                    lootRewards[3].SetActive(true);
                }

                //lootRewards[i].SetActive(true);
                if (overWorldEnemies[i].activeSelf == true)
                {
                    overWorldEnemies[i].SetActive(false);
                } 
            }
        }

        if(inkPortals[3].activeSelf == false)
        {
            caveDoorBlockers[0].SetActive(false);
            caveEntraceColliders[0].SetActive(true);
        }

        if (PlayerMovement.barrelQuestItemGiven == true || PlayerMovement.barrelQuestItemPickedUp == true)
        {
            questBarrel.SetActive(false);
        }
    }
}
