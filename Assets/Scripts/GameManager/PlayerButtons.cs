using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerButtons : MonoBehaviour
{
    public Image paintSlider, specialButton;

    //Active parties
    private Entity[] a, targetedParty;
    private Enemy[] e;
    
    private int select;

    //Accessed classes
    private CombatSystem cs;

    //Buttons for moves
    public Vector2[] pos;
    public Canvas c;
    public int[] btnWhitelist;
    public Button[] tB;
    private Moves[] currentMoves;
    private int targetCount = 0, maxTargets = 0;
    private Entity[] targets = new Entity[7];
    private bool isAllyEntity = false;
    

    private void Awake()
    {
        cs = FindObjectOfType<CombatSystem>();
    }

    public void LoadMoves(Moves[] m, bool isAlly, Enemy[] enemies, Entity[] ally, int current)
    {
        e = enemies;
        a = ally;
        currentMoves = m;
        isAllyEntity = isAlly;

        if (isAllyEntity)
        {
            c.gameObject.SetActive(true);
            c.transform.position = pos[current];

            for (int i = 0; i < m.Length; i++)
            {
                if(currentMoves.Length > 2)
                {
                    c.transform.GetChild(i).gameObject.name = i.ToString();
                    c.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    c.transform.GetChild(btnWhitelist[i]).gameObject.name = i.ToString();
                    c.transform.GetChild(btnWhitelist[i]).gameObject.SetActive(true);
                }
                
            }
        }
        else
        {
            int r1 = Random.Range(0, m.Length);
            if(!currentMoves[r1].isFriendlyTarget)
            {
                int r2 = Random.Range(0, a.Length);
                targets[0] = a[r2];
            }
            else
            {
                int r2 = Random.Range(0, e.Length);
                targets[0] = e[r2];
            }
            
            cs.UseMove(targets, currentMoves[r1], 1);
        }
    }

    public void SelectMove()
    {
        int.TryParse(EventSystem.current.currentSelectedGameObject.name, out select);

        if (isAllyEntity && currentMoves[select].cost > CombatSystem.p.currentPaint)
        {
            StartCoroutine("OutOfPaint");
            return;
        }

        for (int i = 0; i < currentMoves.Length; i++)
            c.transform.GetChild(i).gameObject.SetActive(false);
        c.gameObject.SetActive(false);

        int mod;

        if (currentMoves[select].isFriendlyTarget)
        {
            targetedParty = a;
            mod = 0;
        }
        else
        {
            targetedParty = e;
            mod = 4;
        }

        maxTargets = currentMoves[select].targets.Length;
        if (maxTargets == 0) ((Run)currentMoves[select]).RunAway();
        else
        {
            for (int i = 0; i < targetedParty.Length; i++)
            {
                tB[i + mod].GetComponentInChildren<TextMeshProUGUI>().name = i.ToString();
                tB[i + mod].gameObject.SetActive(true);
            }
        }
    }

    public void SelectTargets()
    {
        int.TryParse(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name, out int t);
        targets[targetCount] = targetedParty[t];
        targetCount++;
        tB[select].gameObject.SetActive(false);

        if (targetCount == maxTargets || targetCount == targetedParty.Length)
        {
            targetCount = 0;

            for (int i = 0; i < tB.Length; i++)
                tB[i].gameObject.SetActive(false);

            cs.CheckForMiniGame(targets, currentMoves[select]);
        }
    }

    private IEnumerator OutOfPaint()
    {
        paintSlider.color = Color.red;
        specialButton.color = Color.red;
        yield return new WaitForSeconds(.25f);
        paintSlider.color = Color.white;
        specialButton.color = Color.white;
    }
}
