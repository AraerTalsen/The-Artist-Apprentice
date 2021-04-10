using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryStats : MonoBehaviour
{
    public Player player;
    public Text playerHealth, playerAttackValue, playerPaintAmount;

    // Update is called once per frame
    void Update()
    {
        playerHealth.text = "Current Health: " + player.currentHP + "/" + player.maxHP;
        playerAttackValue.text = "Current Attack Value: " + player.HitValue;
        playerPaintAmount.text = "Current Paint Value: " + player.currentPaint + "/" + player.maxPaint;
    }
}
