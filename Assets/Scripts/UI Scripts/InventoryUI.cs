using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] CombatMinionInventory;

    public Button[] buttons;

    public GameObject inventoryUIObject;

    public static bool inventoryOpen = true;
    public static bool buttonPressed = false;

    public static bool minion1Removed = false;
    public static bool minion2Removed = false;
    public static bool minion3Removed = false;

    public bool openInvOnce = true;

    public Transform caseMovePoint;
    public Transform caseRetractPoint;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = inventoryUIObject.GetComponent<Animator>();
        //DontDestroyOnLoad(transform.gameObject);
        StartCoroutine(FixInvBug());
    }

    IEnumerator FixInvBug()
    {
        inventoryOpen = true;
        inventoryUIObject.SetActive(true);
        //inventoryUIObject.transform.localPosition = new Vector2(1000, 1000);
        yield return new WaitForEndOfFrame();
        inventoryOpen = false;
        //inventoryUIObject.transform.localPosition = new Vector2(0, 0);
        inventoryUIObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.menuOpen == true)
        {
            //do nothing, don't open if the pause menu is open
        } else
        {
            if (Input.GetKeyUp(KeyCode.Tab) && inventoryOpen == false)
            {
                StartCoroutine(OpenInventory());


            }
            else if (Input.GetKeyUp(KeyCode.Tab) && inventoryOpen == true)
            {
                StartCoroutine(CloseInventory());

            }

            if (ListCreator.numMinionInCombat == 0)
            {
                CombatMinionInventory[0].GetComponent<Image>().enabled = false;
                CombatMinionInventory[1].GetComponent<Image>().enabled = false;
                CombatMinionInventory[2].GetComponent<Image>().enabled = false;
            }
            if (ListCreator.numMinionInCombat == 1)
            {
                CombatMinionInventory[0].GetComponent<Image>().enabled = true;
                CombatMinionInventory[1].GetComponent<Image>().enabled = false;
                CombatMinionInventory[2].GetComponent<Image>().enabled = false;
            }
            if (ListCreator.numMinionInCombat == 2)
            {
                CombatMinionInventory[0].GetComponent<Image>().enabled = true;
                CombatMinionInventory[1].GetComponent<Image>().enabled = true;
                CombatMinionInventory[2].GetComponent<Image>().enabled = false;
            }
            if (ListCreator.numMinionInCombat == 3)
            {
                CombatMinionInventory[0].GetComponent<Image>().enabled = true;
                CombatMinionInventory[1].GetComponent<Image>().enabled = true;
                CombatMinionInventory[2].GetComponent<Image>().enabled = true;
            }
        }

    }

    IEnumerator OpenInventory()
    {
        anim.Play("Inventory - Painters Case");
        PlayerMovement.pauseGame = true;
        inventoryOpen = true;
        inventoryUIObject.SetActive(true);
        LeanTween.move(inventoryUIObject, caseMovePoint, 0.3f);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/Sweep");

        yield return new WaitForSeconds(1f);
    }

    IEnumerator CloseInventory()
    {
        anim.Play("Inventory - Close Painters Case");

        yield return new WaitForSeconds(1f);

        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/SweepDown");
        LeanTween.move(inventoryUIObject, caseRetractPoint, 0.3f);

        yield return new WaitForSeconds(0.5f);

        PlayerMovement.pauseGame = false;
        inventoryOpen = false;
        inventoryUIObject.SetActive(false);
    }

}
