using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControlInventoryInformation : MonoBehaviour
{
    public static GlobalControlInventoryInformation Instance;

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
