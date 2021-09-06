using UnityEngine;
using UnityEngine.UI;

public class InventoryTabSystem : MonoBehaviour
{
    public GameObject MinionPanel, PlayerPanel, ItemsPanel, QuestPanel;

    public void EnableMinionPanel()
    {
        MinionPanel.SetActive(true);
        PlayerPanel.SetActive(false);
        ItemsPanel.SetActive(false);
        QuestPanel.SetActive(false);

    }

    public void EnablePlayerPanel()
    {
        MinionPanel.SetActive(false);
        PlayerPanel.SetActive(true);
        ItemsPanel.SetActive(false);
        QuestPanel.SetActive(false);
    }

    public void EnableItemsPanel()
    {
        MinionPanel.SetActive(false);
        PlayerPanel.SetActive(false);
        ItemsPanel.SetActive(true);
        QuestPanel.SetActive(false);
    }

    public void EnableQuestPanel()
    {
        MinionPanel.SetActive(false);
        PlayerPanel.SetActive(false);
        ItemsPanel.SetActive(false);
        QuestPanel.SetActive(true);
    }

}
