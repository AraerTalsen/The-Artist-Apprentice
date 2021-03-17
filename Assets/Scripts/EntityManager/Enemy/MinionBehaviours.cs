using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MinionBehaviours : MonoBehaviour
{
    //Minion components
    public static int numMinions = 0;
    public Image[] minionHUDS;
    public Vector2[] spawnPoints;
    public GameObject minionBody;
    private GameObject[] minionBodies = new GameObject[3];
    public Enemy[] minions = new Enemy[3];
    public GameObject[] bodies;

    //Accessed libraries
    private PlayerButtons pb;
    private CombatSystem cs;
    private UpdateHUD uh;

    private void Start()
    {
        pb = FindObjectOfType<PlayerButtons>();
        cs = FindObjectOfType<CombatSystem>();
        uh = FindObjectOfType<UpdateHUD>();
    }

    public void NewMinion()
    {
        if(ListCreator.combatMinionsList.Count != 0)
        {
            if (ListCreator.minion1Active == true)
            {
                minions[numMinions] = Instantiate(EnemyLibrary.ChooseEnemy(0));
                ListCreator.minion1Active = false;
                ListCreator.callMinion1 = false;
            }

            if (ListCreator.minion2Active == true)
            {
                minions[numMinions] = Instantiate(EnemyLibrary.ChooseEnemy(1));
                ListCreator.minion2Active = false;
                ListCreator.callMinion2 = false;
            }

            if (ListCreator.minion3Active == true)
            {
                minions[numMinions] = Instantiate(EnemyLibrary.ChooseEnemy(2));
                ListCreator.minion3Active = false;
                ListCreator.callMinion3 = false;
            }
        }

        //Randomly do the minions for the player
        //minions[numMinions] = Instantiate(EnemyLibrary.ChooseEnemy(Random.Range(0, 3))); //Minion brain is created

        numMinions++;
        //pb.SetAllyToButtons(numMinions);
        uh.AddAlly(minions[numMinions - 1]);

        //hard set minion attack value
        minions[numMinions - 1].HitValue = 1;

        CombatSystem.allyParty[numMinions] = minions[numMinions - 1];
    }

    public void MinionTurn(int index, Enemy[] e, Entity[]a)
    {
        if (index - 1 < bodies.Length)
        {
            bodies[index - 1].GetComponent<MinionAnimScript>().MinionAnimTime();
        }

        if (index <= numMinions && !CombatSystem.allyParty[index].isDead)
        {
            //pb.PlayerNewTurn(index, e, a);

            //cs.EnemyDeadCheck();
        }
        else if(index > numMinions)
        {
            for (int i = 0; i < bodies.Length; i++)
            {
                bodies[i].GetComponent<MinionAnimScript>().MinionRetract();
            }

            //cs.EnemyDeadCheck();
            cs.StartCoroutine("EnemyTurn"); //switch to the Enemy Turn Function with a small delay
        }
    }

    //Invoke can only be called on a method in the same class, but Enemy Turn is in a different class.
    public void EnemyTurn() { cs.EnemyTurn(); } 
}
