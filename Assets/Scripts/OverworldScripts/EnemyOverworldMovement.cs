using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyOverworldMovement : MonoBehaviour
{
    public int num;
    public float speed;
    public Vector2 target;
    public Vector2 position;
    public GameObject PlayerPosition;
    public Enemy[] party = new Enemy[3];//Which enemies will appear in combat
    public GameObject[] lootDrops;

    public bool playerInRange = false;

    private Animator anim;
    private Rigidbody2D rb;

    public bool newValue = true;
    public bool canPatrol = true;
    public bool canMove = true;
    public float moveX;
    public float moveY;

    public GameObject alertSprite;

    public Animator transition;

    private void Start()
    {
        for(int i = 0; i < party.Length; i++)
        {
            party[i] = Instantiate(party[i]);
        }

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            canMove = false;

            alertSprite.SetActive(true);
            //StartCoroutine(Alert());

            anim.SetBool("isMoving", true);

            float step = speed * Time.deltaTime;
            target = new Vector2(PlayerPosition.transform.position.x, PlayerPosition.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }

        if (!playerInRange && canPatrol)
        {
            StartCoroutine(Patrol());
        }

        if (canMove)
        {
            anim.SetBool("isMoving", true);

            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveX, moveY), step);
        }

        //if(rb.velocity.y > 0)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1); //flip the sprite
        //}
        //else
        //{
        //    transform.localScale = new Vector3(1, 1, 1); //flip the sprite
        //}
    }

    IEnumerator Patrol()
    {
        canPatrol = false;
        if(newValue == true)
        {
            NewValue();
        }

        anim.SetBool("isMoving", false);
        canMove = false;

        yield return new WaitForSeconds(2f);

        canMove = true;

        yield return new WaitForSeconds(2f);

        canPatrol = true;
        newValue = true;
    }

    //IEnumerator Alert()
    //{
    //    alertSprite.SetActive(true);

    //    yield return new WaitForSeconds(1f);

    //    alertSprite.SetActive(false);
    //}

    public void NewValue()
    {
        moveX = Random.Range(-10f, 10f);
        moveY = Random.Range(-10f, 10f);
        newValue = false;
    }

    //Start combat if collide with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            PlayerMovement.pauseGame = true;
            playerInRange = false;
            CombatSystem.enemyParty = party;
            //LocationRememberer.awokenDim[FindObjectOfType<LocationLoader>().num] = true;
            LoadNextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/Inventory/ShakeLeft");
            FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/InkMonster");

            alertSprite.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            alertSprite.SetActive(false);
            playerInRange = false;
            anim.SetBool("isMoving", false);
        }
    }

    public void LoadNextLevel()
    {
        CombatSystem.id = GetComponent<Whackable>().id;
        CombatSystem.enemyParty = party;
        StartCoroutine(LoadLevel("Combat"));
    }

    IEnumerator LoadLevel(string levelIndex)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/EnterCombat");
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Combat");
    }
}
