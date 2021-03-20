using UnityEngine;
using UnityEngine.UI;

public class InventoryTabSystem : MonoBehaviour
{
    public GameObject MinionPanel, PlayerPanel, ItemsPanel, MapPanel;

    public void EnableMinionPanel()
    {
        MinionPanel.SetActive(true);
        PlayerPanel.SetActive(false);
        ItemsPanel.SetActive(false);
        MapPanel.SetActive(false);

    }

    public void EnablePlayerPanel()
    {
        MinionPanel.SetActive(false);
        PlayerPanel.SetActive(true);
        ItemsPanel.SetActive(false);
        MapPanel.SetActive(false);
    }

    public void EnableItemsPanel()
    {
        MinionPanel.SetActive(false);
        PlayerPanel.SetActive(false);
        ItemsPanel.SetActive(true);
        MapPanel.SetActive(false);
    }

    public void EnableMapPanel()
    {
        MinionPanel.SetActive(false);
        PlayerPanel.SetActive(false);
        ItemsPanel.SetActive(false);
        MapPanel.SetActive(true);
    }
}
