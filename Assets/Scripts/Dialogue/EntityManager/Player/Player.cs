using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : Entity
{
    public GameObject playerPrefab;

    public int maxMinions = 3;
    public int currentPaint, maxPaint;
}
//hello this is sean. if youre reading this, not only have i hijacked your project but i will also kiss you on the lips. there is no escape