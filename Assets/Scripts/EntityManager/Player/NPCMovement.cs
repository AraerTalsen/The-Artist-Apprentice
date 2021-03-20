using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float speed;

    public bool playerInRange;
    public bool canPatrol = true;
    public bool canMove = true;
    public bool newValue = true;

    public float moveX;
    public float moveY;

    public GameObject alertSprite;
    public GameObject playerPosition;

    public bool npcHasQuest = true;

    public SpriteRenderer NPCRenderer;

    // Update is called once per frame
    void Update()
    {

        if(npcHasQuest)
        {
            alertSprite.SetActive(true);
        } else
        {
            alertSprite.SetActive(false);
        }

        if (playerInRange)
        {
            canMove = false;
            if(playerPosition.transform.position.x > this.transform.position.x)
            {
                NPCRenderer.flipX = false;
            }
            else
            {
                NPCRenderer.flipX = true;
            }
        }

        if (!playerInRange && canPatrol)
        {
            StartCoroutine(Patrol());
        }

        if (canMove)
        {
            //anim.SetBool("isMoving", true);

            float step = (speed/2) * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveX, moveY), step);
        }
    }

    IEnumerator Patrol()
    {
        canPatrol = false;
        if (newValue == true)
        {
            NewValue();
        }

        //anim.SetBool("isMoving", false);
        canMove = false;

        yield return new WaitForSeconds(2f);

        canMove = true;

        yield return new WaitForSeconds(2f);

        canPatrol = true;
        newValue = true;
    }

    public void NewValue()
    {
        moveX = Random.Range(-13f, 13f);
        moveY = Random.Range(-13f, 13f);
        newValue = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            //anim.SetBool("isMoving", false);
        }
    }
}
