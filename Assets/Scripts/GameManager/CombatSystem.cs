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
    public static Player p;

    public static Enemy[] enemyParty;
    public static Entity[] allyParty;
    private Entity[] all;

    //Entity placements
    public Transform playerSpawn; 
    public Transform[] pos; //Spawn points for enemies

    //Party UI
    public GameObject[] eDisplay; //The panel that enemy info is listed on. [Disable to make everything disabled.]
    public GameObject[] aDisplay; //The panel that ally info is listed on. [Disable to make everything disabled.]
    public Image[] enemyAction;
    public Sprite[] actions;

    //Accessed classes
    private UpdateHUD uh;
    private PlayerButtons pb;

    //State revision
    public static int index;
    public StatusEffect poison;
    public static int livingEnemies;
    public static int numMinions;
    private bool debugSession = false;
    private int currentEntity = 0;
    public int state = 0;
    public static bool isPortal = false;
    


    // Start is called before the first frame update
    void Start()
    {
        GlobalControl.relicFiveCollected = true;
        p = player1;
        EnemyPartyBuilder();

        uh = FindObjectOfType<UpdateHUD>();
        pb = FindObjectOfType<PlayerButtons>();

        SetUpCombat();
    }

    //Initiates enemy party
    public void EnemyPartyBuilder()
    {
        //When a battle starts from a level, the enemy pumps data to the enemy party array. If the battle is initiated from
        //the Combat scene, a debugging party is loaded
        if (enemyParty == null || enemyParty.Length == 0)
        {
            enemyParty = new Enemy[dE.Length];

            for (int i = 0; i < enemyParty.Length; i++)
            {
                enemyParty[i] = Instantiate(dE[i]);
            }
            debugSession = true;
        }
        else debugSession = false;
        livingEnemies = enemyParty.Length;

        //Attaches visual component of enemy to Scriptable Object
        for (int i = 0; i < enemyParty.Length; i++)
        {
            GameObject g = Instantiate(enemyParty[i].baseBody);
            g.transform.SetParent(eDisplay[i].transform);//Attaches visual component of enemy to Scriptable Object
            g.transform.localPosition = Vector2.zero;
            CombatAnim ca = g.GetComponent<CombatAnim>();
            ca.movePoint = g.transform.parent.GetChild(1);
            ca.retractPoint = g.transform.parent.GetChild(2);
            enemyParty[i].currentBody = g;
        }
    }

    //Initiates ally party
    public void AllyPartyBuilder()
    {
        //When a battle starts from a level, the player pumps data to the ally party array. If the battle is initiated from
        //the Combat scene, a debugging party is loaded

        List<string> allies = ListCreator.combatMinionsList;

        numMinions = debugSession ? 3 : allies.Count;//Check if debug session

        allyParty = new Entity[numMinions + 1];
        all = new Entity[numMinions + livingEnemies + 1];//Array to hold all entities on the field
        allyParty[0] = player1;
        GameObject gameobject = Instantiate(allyParty[0].baseBody);
        gameobject.transform.SetParent(aDisplay[0].transform);//Attaches visual component of enemy to Scriptable Object
        gameobject.transform.localPosition = Vector2.zero;
        CombatAnim ca = gameobject.GetComponent<CombatAnim>();
        ca.movePoint = gameobject.transform.parent.GetChild(1);
        ca.retractPoint = gameobject.transform.parent.GetChild(2);
        all[0] = player1;
        allyParty[0].currentBody = gameobject;
        for (int i = 1; i <= numMinions; i++)
        {
            //Loads either defaults or specified enemies depening on whether it is a debug session
            allyParty[i] = debugSession ? Instantiate(dA[i - 1]) : Instantiate((Entity)Resources.Load("Enemies/" + allies[i - 1], typeof(Object)));
            allyParty[i].isAlly = true;
            GameObject g = Instantiate(allyParty[i].baseBody);
            g.transform.SetParent(aDisplay[i].transform);//Attaches visual component of enemy to Scriptable Object
            g.transform.localPosition = Vector2.zero;
            ca = g.GetComponent<CombatAnim>();
            ca.movePoint = g.transform.parent.GetChild(1);
            ca.retractPoint = g.transform.parent.GetChild(2);
            all[i] = allyParty[i];
            allyParty[i].currentBody = g;
        }
        //((Player)allyParty[0]).currentPaint = ((Player)allyParty[0]).currentPaint;
    }

    private void SetUpCombat()
    {
        AllyPartyBuilder();

        for (int i = allyParty.Length; i < all.Length; i++)
        {
            all[i] = enemyParty[i - allyParty.Length];
        }

        uh.LoadHUDs();
        uh.UpdateEveryHUD();

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
                    {
                        StatusEffect sf = all[currentEntity].statusEffect[0];
                        sf.Effect();
                        StartCoroutine(sf.ColorBlink());
                    }
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
                break;
            }
            case 4:
            {
                state = 0;
                currentEntity = currentEntity == all.Length - 1 ? 0 : currentEntity + 1;
                StateMachine();
                break;
            }
        }  
    }

    private void ChooseMove()
    {
        if (!all[currentEntity].isDead)
            pb.LoadMoves(all[currentEntity].moveList, all[currentEntity].isAlly, enemyParty, allyParty, currentEntity);
        else
        {
            state = 4;
            StateMachine();
        }
    }

    public void CheckForMiniGame(Entity[] targets, Moves m)
    {
        if (all[currentEntity].isAlly && m.miniGame != null)
        {
            GameObject g = Instantiate(m.miniGame);

            MinigameData.targets = targets;
            MinigameData.m = m;
            MinigameData.cs = this;
            MinigameData.miniGameBody = g;
        }
        else
            UseMove(targets, m, 1);
    }

    public void UseMove(Entity[] targets, Moves m, int mod)
    {
        for (int i = 0; i < m.targets.Length; i++)
        {
            targets[i].currentHP += all[currentEntity].HitValue * m.val * mod;


            bool relicEffect = currentEntity == 0 && GlobalControl.relicFiveCollected;
            StatusEffect sf = !relicEffect ? m.sf : poison;
            if (sf != null && targets[i].statusEffect[sf.activationPeriod] == null)
            {
                sf = Instantiate(sf);
                targets[i].statusEffect[sf.activationPeriod] = sf;
                sf.host = targets[i];
                sf.SetIcon(true);
            }

            StartCoroutine(ColorBlink(m, targets[i]));

            if (all[currentEntity].isAlly)
                player1.currentPaint -= m.cost;
            else
                enemyAction[currentEntity - allyParty.Length].sprite = m.moveType;

            StartCoroutine("TelegraphMove");

        }
    }

    private IEnumerator TelegraphMove()
    {
        all[currentEntity].currentBody.GetComponent<CombatAnim>().AnimTime();
        yield return new WaitForSeconds(1f);
        all[currentEntity].currentBody.GetComponent<CombatAnim>().Retract();
        yield return new WaitForSeconds(1f);
        state = 3;//When multiple targets can happen, this has to move
        StateMachine();
    }

    private IEnumerator ColorBlink(Moves m, Entity target)
    {
        GameObject body = target.currentBody;
        m.PlaySFX();
        SpriteRenderer bodyHue = body.GetComponent<SpriteRenderer>();
        bodyHue.color = m.effectColor;
        yield return new WaitForSeconds(1);
        bodyHue.color = Color.white;
    }

    private void DeadCheck()
    {
        bool ended = false;
        ended = PlayerDeadCheck(ended);
        if(!ended) ended = EnemyDeadCheck(ended);

        uh.UpdateEveryHUD();

        if (!ended)
        {
            state = 4;
            StateMachine();
        }
    }

    private bool PlayerDeadCheck(bool ended)
    {
        int dead = 0;
        for (int i = 0; i < allyParty.Length; i++)
        {
            if (allyParty[i] != null && allyParty[i].currentHP <= 0 && !allyParty[i].isDead)
            {
                if (i == 0)
                {
                    EndCombat(false);
                    return true;
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
        return false;
    }

    public bool EnemyDeadCheck(bool ended)
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
            return true;
        }
        else return false;
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
                ActiveOverworldEntity.entityInDimension[1][0][id] = false;
                if(isPortal)
                {
                    PaintInfo.index = index;
                    isPortal = false;
                }
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