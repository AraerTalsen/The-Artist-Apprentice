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

    public Entity[] targets;
    public Moves m;
    public CombatSystem cs;
    public GameObject miniGameBody;

    // Start is called before the first frame update
    void Start()
    {
        //canPress = true;
        targets = MinigameData.targets;
        m = MinigameData.m;
        cs = MinigameData.cs;
        miniGameBody = MinigameData.miniGameBody;
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
            StartCoroutine("DestroyMiniGame");
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
            StartCoroutine("DestroyMiniGame");
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
            StartCoroutine("DestroyMiniGame");
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
            StartCoroutine("DestroyMiniGame");
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

    private IEnumerator DestroyMiniGame()
    {
        yield return new WaitForSeconds(.75f);

        if (success) cs.UseMove(targets, m, 1);
        else
        {
            cs.state = 4;
            cs.StateMachine();
        }
        Destroy(miniGameBody);
    }
}
