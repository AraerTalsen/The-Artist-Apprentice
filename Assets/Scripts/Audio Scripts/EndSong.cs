using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSong : MonoBehaviour
{

    private void Awake()
    {

        FMODUnity.RuntimeManager.PlayOneShot("event:/Combat/Music/General Victory Theme");
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
