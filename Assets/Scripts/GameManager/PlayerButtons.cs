using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerButtons : MonoBehaviour
{


    //Active parties
    private Entity[] a, targetedParty;
    private Enemy[] e;
    
    private int select;

    //Accessed classes
    private CombatSystem cs;

    //Buttons for moves
    public Button[] b;
    public Button[] tB;
    private Moves[] currentMoves;
    private int targetCount = 0, maxTargets = 0;
    private Entity[] targets = new Entity[7];
    

    private void Awake()
    {
        cs = FindObjectOfType<CombatSystem>();
    }

    public void LoadMoves(Moves[] m, bool isAlly, Enemy[] enemies, Entity[] ally)
    {
        e = enemies;
        a = ally;
        currentMoves = m;

        if(isAlly)
        {
            for (int i = 0; i < m.Length; i++)
            {
                Text t = b[i].GetComponentInChildren<Text>();
                t.text = m[i].name;
                t.name = i.ToString();
                b[i].gameObject.SetActive(true);
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
            
            cs.UseMove(targets, currentMoves[r1]);
        }
    }

    public void SelectMove()
    {
        int.TryParse(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name, out select);

        for (int i = 0; i < currentMoves.Length; i++)
            b[i].gameObject.SetActive(false);

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

            cs.UseMove(targets, currentMoves[select]);
        }
    }
}
