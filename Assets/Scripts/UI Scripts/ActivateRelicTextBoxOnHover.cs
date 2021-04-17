using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateRelicTextBoxOnHover : MonoBehaviour
{
    public GameObject relicTextDescriptionTextBox;

    public void mouseOverRelic()
    {
        relicTextDescriptionTextBox.SetActive(true);
    }
    public void mouseOffRelic()
    {
        relicTextDescriptionTextBox.SetActive(false);
    }
}
