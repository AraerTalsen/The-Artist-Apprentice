using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemiesAliveHUD : MonoBehaviour
{
    public TextMeshProUGUI t;
    

    public void Start()
    {
        t.text = "Portals Remaining: " + ActiveOverworldEntity.entityCount[1];

        if (ActiveOverworldEntity.entityCount[1] == 0)
        {
            SceneManager.LoadScene("EndDemoScene");
        }
    }
}