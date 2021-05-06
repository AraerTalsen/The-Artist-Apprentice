using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunMicroGame : MonoBehaviour
{
    public GameObject runSliderGO;
    public Slider runSlider;

    public static float runValue;

    public bool startTimer = true;
    public static bool canRun = false;

    public float runDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        runValue = 0;
    }

    private void OnEnable()
    {
        startTimer = true;
        canRun = true;

        runValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        runSlider.value = runValue;
        if (Input.GetKeyUp(KeyCode.Space) && canRun == true)
        {
            runValue = runValue + runDifficulty;
        }

        if(startTimer == true && canRun == true)
        {
            runValue--;
            StartCoroutine(CounterRun());
        }

        if(runValue <= 0)
        {
            runValue = 0;
        }

        if (runValue >= 100)
        {
            runValue = 100;
        }
    }

    IEnumerator CounterRun()
    {
        startTimer = false;
        yield return new WaitForSeconds(1f);
        startTimer = true;
    }
}
