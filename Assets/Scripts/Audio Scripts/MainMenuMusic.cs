using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    //FMOD.Studio.EventInstance MenuMusic;

    private void Awake()
    {

        //MenuMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Overworld/Music/TitleScreen");
    }

    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/Music/TitleScreen");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
