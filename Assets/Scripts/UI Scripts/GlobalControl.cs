using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    //saving inventory data between scenes from this tutorial https://www.sitepoint.com/saving-data-between-scenes-in-unity/

    public static GlobalControl Instance;
    public List<string> itemNames;
    public List<Sprite> itemImages;
    public List<GameObject> itemHolder;
    public List<string> combatMinionsList;
    public static bool relicOneCollected, relicTwoCollected, relicThreeCollected, relicFourCollected, relicFiveCollected, relicSixCollected;
    public static bool inkProofShoesOn = false;
    public static int numDrinksFromFountain;
    public static bool chestKeyCollected, chestOpened;

    public delegate void OnBarrelPickupCallBack(QuestItemProfile questItemProfile);
    public OnBarrelPickupCallBack onBarrelPickupCallBack;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
