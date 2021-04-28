using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateHUDInfo : MonoBehaviour
{
    public Player playerScriptable;

    public TextMeshProUGUI PlayerNameTxt;
    public TextMeshProUGUI CurrentHPTxt;

    public Slider HPSlider;
    public Slider PaintSlider;

    // Start is called before the first frame update
    void Start()
    {
        PlayerNameTxt.text = "Name: " + playerScriptable.eName;
        CurrentHPTxt.text = "" + playerScriptable.currentHP;

        HPSlider.value = playerScriptable.currentHP;
        PaintSlider.value = playerScriptable.currentPaint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
