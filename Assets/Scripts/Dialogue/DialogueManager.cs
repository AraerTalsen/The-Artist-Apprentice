using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }

    public GameObject dialogueBox;
    public GameObject NextButton;

    //public Text dialogueName;
    public TextMeshProUGUI dialogueText;
    //public Image dialoguePortrait;

    public Queue<DialogueBase.Info> dialogueInfo;

    //private bool isDialogueOption;
    public static bool inDialogue;

    public float delay;

    //private Animator portraitAnimator;

    ////options
    //public GameObject DialogueOptionUI;
    private bool isCurrentlyTyping;
    private string completeText;
    //public GameObject []optionButtons;
    //private int optionsAmount;
    //public Text questionText;

    public static bool currentlyTalking = false;

    //public GameObject NextSceneButton;

    private bool buffer = true;

    private void Start()
    {
        dialogueInfo = new Queue<DialogueBase.Info>(); //FIFO Collection

        dialogueBox.SetActive(false);

        //portraitAnimator = dialoguePortrait.GetComponent<Animator>();
    }

    public void EnqueueDialogue(DialogueBase db)
    {
        if (inDialogue) return;
        buffer = true;
        inDialogue = true;
        StartCoroutine(BufferTimer());

        dialogueBox.SetActive(true);
        NextButton.SetActive(true);
        dialogueInfo.Clear();
        currentlyTalking = true;

        if (dialogueBox.activeInHierarchy == true)
        {
            PauseMenu.menuOpen = true;
            InventoryUI.inventoryOpen = true;
        }


        //I have scripts we can use if we ever want to add dialogue option prompts. I am leaving them out for now in order to not over complicate

        //if(db is DialogueOptions)
        //{
        //    isDialogueOption = true;
        //    DialogueOptions dialogueOptions = db as DialogueOptions;
        //    optionsAmount = dialogueOptions.optionsInfo.Length;
        //    questionText.text = dialogueOptions.questionText;

        //    for (int i = 0; i < optionButtons.Length; i++)
        //    {
        //        optionButtons[i].SetActive(false);
        //    }

        //    for (int i = 0; i < optionsAmount; i++)
        //    {
        //        optionButtons[i].SetActive(true);
        //        optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dialogueOptions.optionsInfo[i].buttonText;
        //UnityEventHandler myEventHandler = GetComponent<UnityEventHandler>();
        //myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;
        //        if(dialogueOptions.optionsInfo[i].nextDialogue != null)
        //        {
        //            myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;
        //        }
        //        else
        //        {
        //            myEventHandler.myDialogue = null;
        //        }
        //    }
        //}
        //else
        //{
        //    isDialogueOption = false;
        //}

        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        if (isCurrentlyTyping == true)
        {
            if (buffer == true) return;
            CompleteText();
            StopAllCoroutines();
            isCurrentlyTyping = false;
            return;
        }

        if (dialogueInfo.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();
        completeText = info.myText;

        //dialogueName.text = info.characterName;
        dialogueText.text = info.myText;
        //dialogueText.font = info.myFont;
        //dialoguePortrait.sprite = info.portrait;

        //if(info.portraitToggle == true)
        //{
        //    dialoguePortrait.gameObject.SetActive(true);
        //}
        //else
        //{
        //    dialoguePortrait.gameObject.SetActive(false);
        //}

        dialogueText.text = "";
        //AudioManager.instance.PlayClip(info.myVoice); //one time play
        StartCoroutine(TypeText(info));
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        isCurrentlyTyping = true;
        foreach (char c in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            //AudioManager.instance.PlayClip(info.myVoice); //play every letter
            FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Text Type");
        }
        isCurrentlyTyping = false;
    }

    IEnumerator BufferTimer()
    {
        yield return new WaitForSeconds(0.1f);
        buffer = false;
    }

    private void CompleteText()
    {
        dialogueText.text = completeText;
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);

        inDialogue = false;
        if(inDialogue == false)
        {
            PauseMenu.menuOpen = false;
            InventoryUI.inventoryOpen = false;
        }
        currentlyTalking = false;
        //OptionsLogic();
    }

    //private void OptionsLogic()
    //{
    //    if (isDialogueOption == true)
    //    {
    //        DialogueOptionUI.SetActive(true);
    //    }
    //}

    public void CloseOptions()
    {
        //DialogueOptionUI.SetActive(false);
    }
}
