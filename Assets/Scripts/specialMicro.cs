using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class specialMicro : MonoBehaviour
{
    public Slider specialSlider;
    public GameObject specialSliderGO;

    public bool spaceDown;
    public bool canPress;

    public bool success;
    public TextMeshProUGUI successText;

    // Start is called before the first frame update
    void Start()
    {
        //canPress = true;
    }

    private void OnEnable()
    {
        successText.text = "";
        canPress = true;
    }

    private void OnDisable()
    {
        canPress = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPress == true)
        {
            spaceDown = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            checkSlider();
        }

        if (spaceDown == true)
        {
            specialSlider.value++;
        }

        if(specialSlider.value == 100)
        {
            checkSlider();
        }
    }

    public void checkSlider()
    {
        Debug.Log("Check Slider");

        canPress = false;

        spaceDown = false;

        if (specialNeedle.needleValue == 0)
        {
            if (specialSlider.value >= 20 && specialSlider.value <= 30)
            {
                success = true;
            }
            else
            {
                success = false;
            }
        }
        else if (specialNeedle.needleValue == 1)
        {
            if (specialSlider.value >= 45 && specialSlider.value <= 55)
            {
                success = true;
            }
            else
            {
                success = false;
            }
        }
        else if (specialNeedle.needleValue == 2)
        {
            if (specialSlider.value >= 70 && specialSlider.value <= 80)
            {
                success = true;
            }
            else
            {
                success = false;
            }
        }
        else if (specialNeedle.needleValue == 3)
        {
            if (specialSlider.value >= 95)
            {
                success = true;
            }
            else
            {
                success = false;
            }
        }

        if (success == true)
        {
            successText.text = "Success";
        }
        else
        {
            successText.text = "Fail";
        }
    }
}
