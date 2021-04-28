using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NamePopUp : MonoBehaviour
{
    public TMP_InputField nameInputField;

    public TMP_Text placeholderTxt;

    public GameObject nameInputFieldGO;
    public GameObject continueButton;

    public Player PlayerName;

    // Start is called before the first frame update
    void Start()
    {
        //nameInputFieldGO.SetActive(false);
        continueButton.SetActive(false);
    }

    public void nameTime()
    {
        nameInputFieldGO.SetActive(true);
    }

    public void ContinuePopUp()
    {
        continueButton.SetActive(true);
    }

    public void ContinueButtonPress()
    {
        FMODUnity.RuntimeManager.PlayOneShot(GameAudio.Instance.Heal);
        if (nameInputField.text == "")
        {
            PlayerName.eName = placeholderTxt.text;
        }
        else
        {
            PlayerName.eName = nameInputField.text;
        }

        StartCoroutine(DelayNewScene());
    }

    IEnumerator DelayNewScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
