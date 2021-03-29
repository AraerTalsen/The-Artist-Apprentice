using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateHUD : MonoBehaviour
{
    public GameObject[] eNodes;
    public Image[] eDisplay; //The panel that enemy info is listed on. [Disable to make everything disabled.]
    public Image[] aDisplay; //The panel that ally info is listed on. [Disable to make everything disabled.]

    //Allies
    public TextMeshProUGUI[] allyHP;
    public Slider[] allyHPSlider;
    public Slider paintSlider;
    public SpriteRenderer[] aSprites;

    //Enemies
    public TextMeshProUGUI[] enemyHP;
    public Slider[] enemyHPSlider;
    public SpriteRenderer[] eSprites;

    // Update is called once per frame
    public void UpdateEveryHUD()
    {
        UpdateAllyHUD();
        UpdateEnemyHUD();
    }

    private void UpdateAllyHUD()
    {
        Entity[] a = CombatSystem.allyParty;
        int numAllies = CombatSystem.numMinions + 1;

        for (int i = 0; i < a.Length; i++)
        {
            if (!a[i].isDead)
            {
                allyHP[i].text = "HP: " + a[i].currentHP;
                allyHPSlider[i].value = a[i].currentHP;
            }
        }
        paintSlider.value = ((Player)a[0]).currentPaint;
    }

    private void UpdateEnemyHUD()
    {
        Enemy[] e = CombatSystem.enemyParty;

        for (int i = 0; i < e.Length; i++)
        {
            if(!e[i].isDead)
            {
                enemyHP[i].text = "HP: " + e[i].currentHP;
                enemyHPSlider[i].value = e[i].currentHP;
            }
        }
    }

    public void LoadHUDs()
    {
        Entity[] a = CombatSystem.allyParty;
        int numAllies = CombatSystem.numMinions + 1;

        aDisplay[0].gameObject.SetActive(true);

        aDisplay[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name: " + a[0].eName;
        aDisplay[0].transform.GetChild(1).GetComponent<Slider>().maxValue = a[0].maxHP;
        paintSlider.maxValue = ((Player)a[0]).currentPaint;

        for (int i = 1; i < numAllies; i++)
        {
            Enemy c = (Enemy)a[i];
            aDisplay[i].gameObject.SetActive(true);
            aDisplay[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name: " + c.eName;
            aDisplay[i].transform.GetChild(1).GetComponent<Slider>().maxValue = c.maxHP;
            aSprites[i - 1].sprite = ((Enemy)(a[i])).enemySprite;
        }

        Enemy[] e = CombatSystem.enemyParty;
        for (int i = 0; i < e.Length; i++)
        {
            eNodes[i].gameObject.SetActive(true);

            eDisplay[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name: " + e[i].eName;
            eDisplay[i].transform.GetChild(1).GetComponent<Slider>().maxValue = e[i].maxHP;
            eSprites[i].sprite = e[i].enemySprite;
        }
    }

    public void AddAlly(Enemy a)
    {
        int numAllies = CombatSystem.numMinions;

        aDisplay[numAllies].gameObject.SetActive(true);

        aDisplay[numAllies].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name: " + a.eName;
        aDisplay[numAllies].transform.GetChild(1).GetComponent<Slider>().maxValue = a.maxHP;
        aDisplay[numAllies].transform.GetChild(2).GetComponent<Image>().sprite = a.enemySprite;
    }
}
