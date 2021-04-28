using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//player overworld movement script
public class PlayerMovement : MonoBehaviour
{
    public GameObject hitRange;

    public Player playerScriptableObject;

    public int num;
    public Vector2 facing = Vector2.down;
    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer rend;

    //sets the enemy that the player collided with - loads that enemy type
    public static bool enemy1Combat, enemy2Combat, enemy3Combat = false;

    //sets the speed the player moves at
    public float playerSpeed = 10.0f;
    public float speedStore;
    public float slowedSpeed;

    public ListCreator UpdateMinionInventoryFunction;

    public static bool firstTime = true;
    private static bool playerExists = false;

    public DialogueManager DM;

    public static bool pauseGame = false;
    private bool swing = false;

    public static bool barrelQuestItemPickedUp = false;
    public static bool barrelQuestItemGiven = false;

    public GameObject questCompletepopup;
    public GameObject itemPickedUpPopUp;

    public static int mostRecentQuestComplete = 0;

    public GameObject ObjectWhacked;

    public static bool displayPaintGain;
    public float displayPaintGainTimer;

    public bool inCave;

    void Start()
    {
        //if (!playerExists)
        //{
        //    playerExists = true;
        //    //When a new level is loaded Player does not get deleted
        //    DontDestroyOnLoad(transform.gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

        enemy1Combat = false;
        enemy2Combat = false;
        enemy3Combat = false;

        rend.enabled = true;

        pauseGame = false;

        speedStore = playerSpeed;
        displayPaintGain = false;
        displayPaintGainTimer = 0;
}

    //Takes the wasd and arrow keys for movement in 4 directions
    void Update()
    {
        LocationRememberer.pos[num] = transform.position;

        if (!DialogueManager.inDialogue && pauseGame == false && !swing)
        {
            //Player Movement//
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                //Up
                facing = Vector2.up;
                body.velocity = new Vector2(0, playerSpeed);
                anim.SetInteger("Direction", 1); //animation change

                HUDDown.HubOpen = false;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                //Down
                facing = Vector2.down;
                body.velocity = new Vector2(0, -playerSpeed);
                anim.SetInteger("Direction", 3);

                HUDDown.HubOpen = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                //Left
                facing = Vector2.right;
                body.velocity = new Vector2(-playerSpeed, 0);
                anim.SetInteger("Direction", 2);
                transform.localScale = new Vector3(-1, 1, 1); //flip the sprite

                HUDDown.HubOpen = false;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                //Right
                facing = Vector2.right;
                body.velocity = new Vector2(playerSpeed, 0);
                anim.SetInteger("Direction", 2);
                transform.localScale = new Vector3(1, 1, 1); //flip the sprite

                HUDDown.HubOpen = false;
            }

            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                //No Input
                body.velocity = new Vector2(0, 0);
                anim.SetInteger("Direction", 0);

                HUDDown.HubOpen = true;
            }

            Whack(); //Check if player wants to whack. If so, whack.

            if (displayPaintGain == true)
            {
                ObjectWhacked.SetActive(true);
                displayPaintGainTimer = displayPaintGainTimer - Time.deltaTime;
                if (displayPaintGainTimer < -1)
                {
                    displayPaintGain = false;
                    ObjectWhacked.SetActive(false);
                    displayPaintGainTimer = 0;
                }
            }
        }
        else if (DialogueManager.inDialogue)
        {
            HUDDown.HubOpen = false;
            body.velocity = new Vector2(0, 0);

            if (Input.GetKeyUp(KeyCode.Space))
            {
                DM.DequeueDialogue();
            }
        }

        if (pauseGame)
        {
            HUDDown.HubOpen = false;

            body.velocity = new Vector2(0,0);
            playerSpeed = 0;
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
            playerSpeed = speedStore;
        }

        if (GlobalControl.inkProofShoesOn == true) {
            slowedSpeed = 5f;
        }

        //Debug.Log(GlobalControl.inkProofShoesOn);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "enemy1Sketch")
        {
            addSeanMinion();
            mostRecentQuestComplete = 1;
            Destroy(other.gameObject);
            questCompletepopup.SetActive(true);

            QuestRewardManagerScript.minion1Destroyed = true;
        }

        if (other.gameObject.tag == "enemy2Sketch")
        {
            addMikeMinion();
            mostRecentQuestComplete = 2;
            Destroy(other.gameObject);
            questCompletepopup.SetActive(true);
            QuestRewardManagerScript.minion2Destroyed = true;
        }

        if (other.gameObject.tag == "enemy3Sketch")
        {
            addDanMinion();
            mostRecentQuestComplete = 3;
            Destroy(other.gameObject);
            questCompletepopup.SetActive(true);
            QuestRewardManagerScript.minion3Destroyed = true;
        }

        if(other.gameObject.tag == "barrelQuestItem")
        {
            Destroy(other.gameObject);
            barrelQuestItemPickedUp = true;
        }

        if (other.gameObject.tag == "InkProofShoes")
        {
            
            Destroy(other.gameObject);
            ItemPickedUp.mostRecentItemPickedUp = 0;
            itemPickedUpPopUp.SetActive(true);
            GlobalControl.relicOneCollected = true;
            GlobalControl.inkProofShoesOn = true;
        }

        if (other.gameObject.tag == "BlueInk")
        {
            Destroy(other.gameObject);
            ItemPickedUp.mostRecentItemPickedUp = 1;
            itemPickedUpPopUp.SetActive(true);
            GlobalControl.relicTwoCollected = true;
            playerScriptableObject.maxPaint = playerScriptableObject.maxPaint + 5;
        }

        if (other.gameObject.tag == "PaintersPalette")
        {
            Destroy(other.gameObject);
            ItemPickedUp.mostRecentItemPickedUp = 2;
            itemPickedUpPopUp.SetActive(true);
            playerScriptableObject.maxPaint = playerScriptableObject.maxPaint + 1;
            playerScriptableObject.maxHP = playerScriptableObject.maxHP + 5;
            playerScriptableObject.HitValue = playerScriptableObject.HitValue + 1;
            GlobalControl.relicThreeCollected = true;
        }

        if (other.gameObject.tag == "MagicBrush")
        {
            Destroy(other.gameObject);
            ItemPickedUp.mostRecentItemPickedUp = 3;
            itemPickedUpPopUp.SetActive(true);
            playerScriptableObject.HitValue = playerScriptableObject.HitValue + 2;
            GlobalControl.relicFourCollected = true;
        }

        if(other.gameObject.tag == "CaveDoorLow")
        {
            this.transform.position = new Vector3(73.5f, 70.75f, 0);
            inCave = true;
        }

        if (other.gameObject.tag == "CaveDoorMiddle")
        {
            this.transform.position = new Vector3(121.5f, 63, 0);
            inCave = true;
        }

        if (other.gameObject.tag == "InsideOfCaveRightDoor")
        {
            this.transform.position = new Vector3(21.5f, 62.75f, 0);
            inCave = false;
        }

        if (other.gameObject.tag == "InsideOfCaveLeftDoor")
        {
            this.transform.position = new Vector3(-10.5f, 40.5f, 0);
            inCave = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "InkTiles")
        {
            if(GlobalControl.inkProofShoesOn == false)
            {
                speedStore = 3f;
            }
            else
            {
                slowedSpeed = 5f;
                speedStore = 5f;
            }
            
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "InkTiles")
        {
            speedStore = 5f;
        }

    }

    private void Whack()
    {
        if(!swing && Input.GetKey(KeyCode.LeftShift))
        {
            StartCoroutine("SwingTime");
        }
    }

    private IEnumerator SwingTime()
    {
        anim.SetInteger("isWhacking", 1);

        body.velocity = new Vector2(0, 0);
        playerSpeed = 0;

        swing = true;

        yield return new WaitForSeconds(1.2f);

        playerSpeed = speedStore;
        anim.SetInteger("isWhacking", 0);
        hitRange.GetComponent<Whack>().active = false;
        //hitRange.SetActive(false);
        swing = false;
    }

    public void MoveHitRange()
    {
        hitRange.transform.localPosition = facing * .5f;
        hitRange.GetComponent<Whack>().active = true;
        //hitRange.SetActive(true);
    }

    public void addDanMinion()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Combat/Summon");
        ListCreator.numberOfItemsCollected++;
        ListCreator.runInventoryUpdate = true;
        UpdateMinionInventoryFunction.InsertDanMinion();
    }

    public void addMikeMinion()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Combat/Summon");
        ListCreator.numberOfItemsCollected++;
        ListCreator.runInventoryUpdate = true;
        UpdateMinionInventoryFunction.InsertMikeMinion();
    }

    public void addSeanMinion()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Combat/Summon");
        ListCreator.numberOfItemsCollected++;
        ListCreator.runInventoryUpdate = true;
        UpdateMinionInventoryFunction.InsertSeanMinion();
    }
   
}
