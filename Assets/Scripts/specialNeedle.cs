using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class specialNeedle : MonoBehaviour
{
    //public TextMeshProUGUI countdownText;

    public GameObject needle;
    public static int needleValue;

    //public Transform zeroPoint;
    public Transform quarterPoint;
    public Transform middlePoint;
    public Transform threeQuarterPoint;
    public Transform fullPoint;

    public float needleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //runNeedleValue = Random.RandomRange(0, 3);
    }

    private void OnEnable()
    {
        needleValue = Random.RandomRange(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (needleValue == 0)
        {
            LeanTween.move(needle, quarterPoint, needleSpeed);
        }
        else if (needleValue == 1)
        {
            LeanTween.move(needle, middlePoint, needleSpeed);
        }
        else if (needleValue == 2)
        {
            LeanTween.move(needle, threeQuarterPoint, needleSpeed);
        }
        else if (needleValue == 3)
        {
            LeanTween.move(needle, fullPoint, needleSpeed);
        }
    }
}
