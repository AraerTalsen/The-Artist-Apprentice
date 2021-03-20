using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player overworld movement script
public class PlayerMovement : MonoBehaviour
{
    public GameObject hitRange;
    //public GameObject hitRange;
    //public GameObject hitRange;

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

    public ListCreator UpdateMinionInventoryFunction;

    public static bool firstTime = true;
    private static bool playerExists = false;

    public Sprite minionSprite;

    public DialogueManager DM;

    public static bool pauseGame = false;
    private bool swing = false;

    public static bool barrelQuestItemPickedUp = false;
    public static bool barrelQuestItemGiven = false;

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

        UpdateMinionInventoryFunction = FindObjectOfType<ListCreator>();

        enemy1Combat = false;
        enemy2Combat = false;
        enemy3Combat = false;

        rend.enabled = true;

        pauseGame = false;

        speedStore = playerSpeed;
}

    //Takes the wasd and arrow keys for movement in 8 directions
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
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                //Down
                facing = Vector2.down;
                body.velocity = new Vector2(0, -playerSpeed);
                anim.SetInteger("Direction", 3);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                //Left
                facing = Vector2.right;
                body.velocity = new Vector2(-playerSpeed, 0);
                anim.SetInteger("Direction", 2);
                transform.localScale = new Vector3(-1, 1, 1); //flip the sprite
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                //Right
                facing = Vector2.right;
                body.velocity = new Vector2(playerSpeed, 0);
                anim.SetInteger("Direction", 2);
                transform.localScale = new Vector3(1, 1, 1); //flip the sprite
            }

            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                //No Input
                body.velocity = new Vector2(0, 0);
                anim.SetInteger("Direction", 0);
            }

            Whack(); //Check if player wants to whack. If so, whack.
        }
        else if (DialogueManager.inDialogue)
        {
            body.velocity = new Vector2(0, 0);

            if (Input.GetKeyUp(KeyCode.Space))
            {
                DM.DequeueDialogue();
            }
        }

        if (pauseGame)
        {
            body.velocity = new Vector2(0,0);
            playerSpeed = 0;
            anim.enabled = false;
        }
        else
        {
            anim.enabled = true;
            playerSpeed = speedStore;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "enemy1Sketch")
        {
            addSeanMinion();
            QuestRewardManagerScript.minion1Destroyed = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "enemy2Sketch")
        {
            addMikeMinion();
            QuestRewardManagerScript.minion2Destroyed = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "enemy3Sketch")
        {
            addDanMinion();
            QuestRewardManagerScript.minion3Destroyed = true;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "barrelQuestItem")
        {
            Destroy(other.gameObject);
            barrelQuestItemPickedUp = true;
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
        Debug.Log(UpdateMinionInventoryFunction == null);
        UpdateMinionInventoryFunction.InsertDanMinion();
    }

    public void addMikeMinion()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Combat/Summon");
        ListCreator.numberOfItemsCollected++;
        ListCreator.runInventoryUpdate = true;
        Debug.Log(UpdateMinionInventoryFunction == null);
        UpdateMinionInventoryFunction.InsertMikeMinion();
    }

    public void addSeanMinion()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Combat/Summon");
        ListCreator.numberOfItemsCollected++;
        ListCreator.runInventoryUpdate = true;
        Debug.Log(UpdateMinionInventoryFunction == null);
        UpdateMinionInventoryFunction.InsertSeanMinion();
    }
}
