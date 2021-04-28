using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void LoadPreCombatScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadCombatScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCastle()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
