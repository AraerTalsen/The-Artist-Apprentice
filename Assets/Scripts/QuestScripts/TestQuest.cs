using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuest : MonoBehaviour
{
    public QuestBase quest;

    // Start is called before the first frame update
    void Start()
    {
        quest.InitializeQuest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
