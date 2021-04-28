using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{

    public DialogueBase DB;

    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.instance.EnqueueDialogue(DB);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
