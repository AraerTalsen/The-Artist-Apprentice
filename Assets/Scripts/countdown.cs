using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class countdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;

    public GameObject runNeedle;
    public int runNeedleValue;

    public Transform zeroPoint;
    public Transform quarterPoint;
    public Transform middlePoint;
    public Transform threeQuarterPoint;
    public Transform fullPoint;

    public CombatSystem cs;
    public GameObject miniGameBody;

    public float needleSpeed;

    public bool runSuccess;

    // Start is called before the first frame update
    void Start()
    {
        cs = MinigameData.cs;
        miniGameBody = MinigameData.miniGameBody;
    }

    private void OnEnable()
    {
        countdownText.text = "";
        StartCoroutine(countdownStart());
    }

    // Update is called once per frame
    void Update()
    {
        if(RunMicroGame.canRun == false)
        {
            if (runSuccess == true)
            {
                countdownText.text = "Success";
            }
            if (runSuccess == false)
            {
                countdownText.text = "Fail";
            }

        }

        if (runNeedleValue == 0)
        {
            LeanTween.move(runNeedle, zeroPoint, needleSpeed);
        }
        else if (runNeedleValue == 1)
        {
            LeanTween.move(runNeedle, quarterPoint, needleSpeed);
        }
        else if(runNeedleValue == 2)
        {
            LeanTween.move(runNeedle, middlePoint, needleSpeed);
        }
        else if(runNeedleValue == 3)
        {
            LeanTween.move(runNeedle, threeQuarterPoint, needleSpeed);
        }
        else if(runNeedleValue == 4)
        {
            LeanTween.move(runNeedle, fullPoint, needleSpeed);
        }
    }

    IEnumerator countdownStart()
    {
        runNeedle.SetActive(false);
        yield return new WaitForSeconds(5f);

        countdownText.text = "3";

        yield return new WaitForSeconds(1f);
        countdownText.text = "2";

        yield return new WaitForSeconds(1f);
        countdownText.text = "1";

        yield return new WaitForSeconds(1f);
        runNeedle.SetActive(true);
        countdownText.text = "";
        RunMicroGame.canRun = false;
        moveNeedle();

    }

    public void moveNeedle()
    {
        runNeedleValue = Random.RandomRange(0, 4);
        
        if (runNeedleValue == 0)
        {
            if (RunMicroGame.runValue <= 100)
            {
                runSuccess = true;
            }
            else
            {
                runSuccess = false;
            }
            StartCoroutine("DestroyMiniGame");
        }
        else if (runNeedleValue == 1)
        {
            if (RunMicroGame.runValue >= 25)
            {
                runSuccess = true;
            }
            else
            {
                runSuccess = false;
            }
            StartCoroutine("DestroyMiniGame");
        }
        else if (runNeedleValue == 2)
        {
            if (RunMicroGame.runValue >= 50)
            {
                runSuccess = true;
                
            }
            else
            {
                runSuccess = false;
            }
            StartCoroutine("DestroyMiniGame");
        }
        else if (runNeedleValue == 3)
        {
            if (RunMicroGame.runValue >= 75)
            {
                runSuccess = true;
            }
            else
            {
                runSuccess = false;
            }
            StartCoroutine("DestroyMiniGame");
        }
        else if (runNeedleValue == 4)
        {
            if (RunMicroGame.runValue == 100 || RunMicroGame.runValue >= 90)
            {
                runSuccess = true;
            }
            else
            {
                runSuccess = false;
            }
            StartCoroutine("DestroyMiniGame");
        }
    }

    private IEnumerator DestroyMiniGame()
    {
        yield return new WaitForSeconds(.75f);

        if (runSuccess) cs.LeaveBattle();
        else
        {
            cs.state = 4;
            cs.StateMachine();
        }
        Destroy(miniGameBody);
    }
}
