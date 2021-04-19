using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicOneButtonScript : MonoBehaviour
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
        Debug.Log(GlobalControl.relicOneCollected);
        if (GlobalControl.relicOneCollected == false)
        {
            buttonImage.sprite = relicNotDiscoveredImage;
        }
        else
        {
            buttonImage.sprite = relicDiscoveredImage;
        }
    }

    public void RelicOneButtonClicked()
    {
        imageComponent = relicDescriptionImage.GetComponent<Image>();
        if (GlobalControl.relicOneCollected == false)
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
