using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatSystem : MonoBehaviour
{
    //Enemy ID
    public static int id;

    //Debug functionality
    public Enemy[] dE;
    public Enemy[] dA;

    //Combative parties
    public Player player1; 

    public static Enemy[] enemyParty; 
    public static Entity[] allyParty = new Entity[4];

    //Entity placements
    public Transform playerSpawn; 
    public Transform[] pos; //Spawn points for enemies

    //Party UI

    public GameObject[] eDisplay; //The panel that enemy info is listed on. [Disable to make everything disabled.]
    public GameObject[] aDisplay; //The panel that ally info is listed on. [Disable to make everything disabled.]
    public Image[] enemyAction;
    public Sprite[] actions;

    public static int livingEnemies;
    private bool debugSession = false;

    //Accessed classes
    private EnemyMoves em;
    private PlayerMoves pm;
    private UpdateHUD uh;
    private PlayerButtons pb;

    public GameObject[] img;

    //State revision
    private Entity[] all;
    private int currentEntity = 0;
    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyParty == null || enemyParty.Length == 0)
        {
            enemyParty = new Enemy[dE.Length];

            for (int i = 0; i < dE.Length; i++)
            {
                enemyParty[i] = Instantiate(dE[i]);
                //enemyParty[i].currentHP = 1;
            }
            debugSession = true;
        }
        else debugSession = false;

        for (int i = 0; i < enemyParty.Length; i++)
            enemyParty[i].body = eDisplay[i].transform.GetChild(0).gameObject;

        livingEnemies = enemyParty.Length;
        
        em = FindObjectOfType<EnemyMoves>();
        pm = FindObjectOfType<PlayerMoves>();
        uh = FindObjectOfType<UpdateHUD>();
        pb = FindObjectOfType<PlayerButtons>();

        SetUpCombat();
    }

    private void SetUpCombat()
    {
        List<string> s = ListCreator.combatMinionsList;
        
        allyParty[0] = player1;
        ((Player)allyParty[0]).currentPaint = ((Player)allyParty[0]).maxPaint;

        if (s == null || debugSession)
        {
            MinionBehaviours.numMinions = 3;
            print("Debug party active");
            for (int i = 1; i < allyParty.Length; i++)
            {
                allyParty[i] = Instantiate(dA[i - 1]);
                allyParty[i].isAlly = true;
            }                  
        }
        else
        {
            MinionBehaviours.numMinions = s.Count;
            for (int i = 1; i <= s.Count; i++)
            {
                allyParty[i] = Instantiate((Entity)Resources.Load("Enemies/" + s[i - 1], typeof(Object)));
                allyParty[i].isAlly = true;
            }
        }

        for (int i = 0; i < allyParty.Length; i++)
            allyParty[i].body = aDisplay[i].transform.GetChild(0).gameObject;

        all = new Entity[allyParty.Length + enemyParty.Length];

        for(int i = 0; i < allyParty.Length; i++)
        {
            all[i] = allyParty[i];
        }

        for (int i = allyParty.Length; i < all.Length; i++)
        {
            all[i] = enemyParty[i - allyParty.Length];
        }

        uh.LoadHUDs();
        uh.UpdateEveryHUD();

        //PlayerTurn();
        StateMachine();
    }


    /*
        Combat cycle is: Player turn, Minion turn, Enemy turn. Check if attacked party has dead members 
        after each opposing party member attacks.
    */

    public void StateMachine()
    {
        switch (state)
        {
            case 0:
            {
                if (all[currentEntity].statusEffect[0] != null)
                    all[currentEntity].statusEffect[0].Effect();
                else
                {
                    state = 1;
                    StateMachine();
                }
                break;
            }
            case 1:
            {
                ChooseMove();
                break;
            }
            case 2:
            {
                break;
            }
            case 3:
            {
                DeadCheck();
                state = 0;
                currentEntity = currentEntity == all.Length - 1 ? 0 : currentEntity + 1;
                StateMachine();
                break;
            }
        }  
        //e.statusEffect[0].Effect();
        //e.statusEffect[1].Effect();
    }

    private void ChooseMove()
    {
        if (!all[currentEntity].isDead)
            pb.LoadMoves(all[currentEntity].moveList, all[currentEntity].isAlly, enemyParty, allyParty);
    }

    public void UseMove(Entity[] targets, Moves m)
    {
        for(int i = 0; i < m.targets.Length; i++)
        {
            targets[i].currentHP += all[currentEntity].HitValue * m.val;

            if (m.sf != null && targets[i].statusEffect[m.sf.activationPeriod] == null)
            {
                StatusEffect sf = Instantiate(m.sf);
                targets[i].statusEffect[m.sf.activationPeriod] = sf;
                sf.host = targets[i];
            }  

            StartCoroutine(ColorBlink(m, targets[i]));
            if (all[currentEntity].isAlly)
            {
                player1.currentPaint -= m.cost;//Change later
                state = 3;//When multiple targets can happen, this has to move
                StateMachine();
            }
            else
            {
                enemyAction[currentEntity - enemyParty.Length - 1].sprite = m.moveType;
                StartCoroutine("SlowTheEnemies");
            }

        }
    }

    private IEnumerator SlowTheEnemies()
    {
        img[currentEntity - enemyParty.Length - 1].GetComponent<enemyCombatAnim>().AnimTime();
        yield return new WaitForSeconds(1f);
        img[currentEntity - enemyParty.Length - 1].GetComponent<enemyCombatAnim>().Retract();
        yield return new WaitForSeconds(1f);
        state = 3;
        StateMachine();
    }

    private IEnumerator ColorBlink(Moves m, Entity target)
    {
        GameObject body = target.body;
        SpriteRenderer bodyHue = body.GetComponent<SpriteRenderer>();
        bodyHue.color = m.effectColor;
        yield return new WaitForSeconds(1);
        bodyHue.color = Color.white;
    }

    private void DeadCheck()
    {
        PlayerDeadCheck();
        EnemyDeadCheck();

        uh.UpdateEveryHUD();
    }

    private void PlayerTurn()
    {
        pm.PlayerDecision(allyParty, enemyParty);
    }

    public void EnemyDeadCheck()
    {
        int dead = 0;
        for (int i = 0; i < enemyParty.Length; i++)
            if (enemyParty[i].currentHP <= 0 && !enemyParty[i].isDead)
            {
                enemyParty[i].isDead = true;
                eDisplay[i].SetActive(false);
                livingEnemies--;
                dead++;
            }
        /*Enemy[] temp = new Enemy[enemyParty.Length - dead];
        int num = 0;

        for(int i = 0; i < enemyParty.Length; i++)
        {
            if (enemyParty[i].currentHP > 0)
            {
                temp[num] = enemyParty[i];
                num++;
            }   
        }
        enemyParty = temp;*/

        //uh.UpdateEveryHUD();

        if (livingEnemies <= 0)
        {
            EndCombat(true);
        }
    }

    //this could later be used to decide what attacks the enemy is doing
    public IEnumerator EnemyTurn()
    {
        for(int i = 0; i < enemyParty.Length; i++)
        {
            if(!enemyParty[i].isDead)
            {
                img[i].GetComponent<enemyCombatAnim>().AnimTime();
                yield return new WaitForSeconds(1f);
                //Enemy move is decided if enemy is alive
                enemyAction[i].sprite = ImageAssign(em.ChooseAction(enemyParty[i]));
                PlayerDeadCheck();
                img[i].GetComponent<enemyCombatAnim>().Retract();
                yield return new WaitForSeconds(1f);
            }
        }

        Invoke("PlayerTurn", 1);
    }

    private void PlayerDeadCheck()
    {
        int dead = 0;
        for (int i = 0; i < allyParty.Length; i++)
        {
            if (allyParty[i] != null && allyParty[i].currentHP <= 0 && !allyParty[i].isDead)
            {
                if (i == 0)
                {
                    print("p");
                    EndCombat(false);
                }
                else
                {
                    allyParty[i].isDead = true;
                    aDisplay[i].SetActive(false);
                    dead++;
                }
            }
        }

        Entity[] temp = new Entity[allyParty.Length - dead];
        int num = 0;

        for (int i = 0; i < allyParty.Length; i++)
        {
            if (allyParty[i].currentHP > 0)
            {
                temp[num] = allyParty[i];
                num++;
            }
        }
        allyParty = temp;

        //uh.UpdateEveryHUD();
    }

    //this is where we would put functionality for if a battle is won or lost (win animations/lose states etc.)
    private void EndCombat(bool won)
    {
        if(won)
        {
            List<string> s = new List<string>();
            for(int i = 1; i < allyParty.Length; i++)
            {
                if (allyParty[i] != null &&!allyParty[i].isDead) s.Add(allyParty[i].eName);
            }

            ListCreator.combatMinionsList = s;

            Debug.Log("You did it!");

            if(!debugSession)
            {
                print(1);
                ActiveOverworldEntity.entityInDimension[1][0][id] = false;
                ActiveOverworldEntity.entityCount[1]--;
            }
            else ActiveOverworldEntity.dim = 1;

            LeaveBattle();
        }
        else
        {
            Debug.Log("You died");
            player1.currentHP = player1.maxHP;

            LeaveBattle();
        }
    }

    public void LeaveBattle()
    {
        SceneManager.LoadScene("LevelOneScene");
    }

    //Displays to player what the enemy did for its attack
    private Sprite ImageAssign(string s)
    {
        switch(s)
        {
            case "Heal":
            {
                return actions[0];
            }
            case "Attack":
            {
                return actions[1];
            }
            case "Buff":
            {
                return actions[2];
            }
            case "Debuff":
            {
                return actions[3];
            }
            default:
            {
                return null;
            }
            
        }
    }
}