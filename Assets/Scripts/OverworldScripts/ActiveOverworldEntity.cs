using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOverworldEntity : MonoBehaviour
{
    public static int dim = 1;
    public static int[] entityCount = { 0, 4 };

    public static List<bool>[][] entityInDimension =
    {
        new List<bool>[]//Castle
        {
            //Fill when it has whackable content
        },

        new List<bool>[]//Desert
        {
            new List<bool>(),//Enemies
            new List<bool>() //Assets
        }
    };

    public static int AddEntity(int type, Whackable w)
    {
        entityInDimension[dim][type].Add(true);
        return entityInDimension[dim][type].Count - 1;
    }

    public static void LoadActive(List<Whackable> w)
    {
        int type = w[0].type;
        for(int j = 0; j < entityInDimension[dim][type].Count; j++)
        {
            if (!entityInDimension[dim][type][j] || !w[j].on)
            {
                if (!w[j].on) entityInDimension[dim][type][j] = false;
                w[j].on = false;
                w[j].gameObject.SetActive(false);
            }
            w[j].id = j;
        }
    }

    private static Whackable findInWorld(Whackable w, Whackable[]whack)
    {
        for(int i = 0; i < whack.Length; i++)
        {
            if (w == whack[i]) return whack[i];
        }

        return null;
    }
}
