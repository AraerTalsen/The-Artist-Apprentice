using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    public Player playerScirptable;

    public int currentHP;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHP = playerScirptable.currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScirptable.currentHP < currentHP)
        {
            PlayerIsHurt();
        }
        else
        {
            anim.SetBool("playerHurt", false);
        }
    }

    public void PlayerIsHurt()
    {
        anim.SetBool("playerHurt", true);
        StartCoroutine(PlayerHurtTimer());
    }

    IEnumerator PlayerHurtTimer()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("playerHurt", false);

        currentHP = playerScirptable.currentHP;
    }
}
