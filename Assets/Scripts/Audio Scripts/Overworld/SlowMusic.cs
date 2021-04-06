using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMusic : MonoBehaviour
{
    public float interactRange = 1.7f;

    private FMOD.Studio.EventInstance instance;

    public float pitchValue;
    public string fmodEvent;

    public float pitchMax;
    public float pitchValueDecreaser;

    // Start is called before the first frame update
    void Start()
    {
        pitchValue = 0f;
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(gameObject.transform.position, OverworldManager.instance.Player.position) < interactRange)
        {
            if (pitchValue >= pitchMax)
            {
                instance.setParameterByName("Slow Music", pitchValue);
                pitchValue = pitchValue - pitchValueDecreaser;
            }
        }
        else
        {
            if (pitchValue <= 0)
            {
                instance.setParameterByName("Slow Music", pitchValue);
                pitchValue = pitchValue + pitchValueDecreaser;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

    private void OnDestroy()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
