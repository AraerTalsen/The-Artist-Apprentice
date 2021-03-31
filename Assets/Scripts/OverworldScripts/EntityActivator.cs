using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityActivator : MonoBehaviour
{
    public GameObject[] whackPack;
    private List<Whackable> whack =  new List<Whackable>();
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < whackPack.Length; i++)
        {
            for(int j = 0; j < whackPack[i].transform.childCount; j++)
            {
                whack.Add(whackPack[i].transform.GetChild(j).GetComponent<Whackable>());
            }
        }

        if (!LocationRememberer.awokenDim[FindObjectOfType<LocationLoader>().num])
        {
            for (int i = 0; i < whack.Count; i++)
                whack[i].id = ActiveOverworldEntity.AddEntity(whack[i].type, whack[i]);
        }
        else
        {
            Alt();
        }     
    }

    private void Alt()
    {
        Debug.Log("this is running alt");
        List<Whackable>[] w =
            {
                new List<Whackable>(),
                new List<Whackable>()
            };

        for (int i = 0; i < whack.Count; i++)
        {
            Debug.Log(whack[i]);
            Debug.Log(whack[i].type);
            Debug.Log(w[whack[i].type]);
            w[whack[i].type].Add(whack[i]);

        }

        for (int i = 0; i < w.Length; i++)
            ActiveOverworldEntity.LoadActive(w[i]);
    }
}
