using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes Singleton Objects within the game, Runs once at game start
/// </summary>

public class GameAudioInitializer : MonoBehaviour
{
    //SingletonSO in build
    [Tooltip("GameAudio SO")]
    public GameAudio gameAudio;


    void Awake()
    {
        //Initializes
        gameAudio = GameAudio.Instance;
    }
}
