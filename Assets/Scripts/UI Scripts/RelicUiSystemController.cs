using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicUiSystemController : MonoBehaviour
{
    public List<string> relicNames;
    public List<string> relicDescriptions;
    public List<Sprite> relicImages;
    public List<Text> relicNameTextBox;
    public List<Text> relicDescriptionTextBox;

    public List<GameObject> relics;
    public static int relicsCollectedCounter;

    private Image imageComponent;

    public void UpdateRelicUI()
    {
        for (int i = 0; i < relicsCollectedCounter; i++)
        {
            relics[i].SetActive(true);
            relicNameTextBox[i].text = relicNames[i];
            relicDescriptionTextBox[i].text = relicDescriptions[i];
            imageComponent = relics[i].GetComponent<Image>();
            imageComponent.sprite = relicImages[i];
        }
    }
}
