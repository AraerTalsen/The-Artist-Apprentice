using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicFourButtonScript : MonoBehaviour
{
    public Sprite relicNotDiscoveredImage, relicDiscoveredImage;
    public Text relicDescriptionTextBox;
    public GameObject relicDescriptionImage;

    public string relicDescription, relicNotDiscoveredDescription;

    private Image imageComponent;
    public Image buttonImage;

    // Update is called once per frame
    void Update()
    {
        if (GlobalControl.relicFourCollected == false)
        {
            buttonImage.sprite = relicNotDiscoveredImage;
        }
        else
        {
            buttonImage.sprite = relicDiscoveredImage;
        }
    }

    public void RelicTwoButtonClicked()
    {
        imageComponent = relicDescriptionImage.GetComponent<Image>();
        if (GlobalControl.relicFourCollected == false)
        {
            imageComponent.sprite = relicNotDiscoveredImage;
            relicDescriptionTextBox.text = relicNotDiscoveredDescription;
        }
        else
        {
            imageComponent.sprite = relicDiscoveredImage;
            relicDescriptionTextBox.text = relicDescription;

        }
    }
}
