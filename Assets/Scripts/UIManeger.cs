using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManeger : MonoBehaviour
{
    [SerializeField] GameObject resultPanel;
    [SerializeField] Text resultText;

    //ヒーローテキスト
    [SerializeField] Text playerHeroHpText;
    [SerializeField] Text enemyHeroHpText;

    //マナコストUI
    [SerializeField] Text playerManaCostText;
    [SerializeField] Text enemyManaCostText;

    //時間表示UI
    [SerializeField] Text timeCountText;

    public void HideResutPanel()
    {
        resultPanel.SetActive(false);
    }

    public void ShowHeroHP(int playerHeroHp, int enemyHeroHp)
    {
        playerHeroHpText.text = playerHeroHp.ToString();
        enemyHeroHpText.text = enemyHeroHp.ToString();
    }

    public void ShowManaCost(int playerManaCost, int enemyManaCost)
    {
        playerManaCostText.text = playerManaCost.ToString();
        enemyManaCostText.text = enemyManaCost.ToString();
    }

    public void UpdateTime(int timeCout)
    {
        timeCountText.text = timeCout.ToString();
    }

    public void ShowResultPanel(int playerHeroHp)
    {
        resultPanel.SetActive(true);
        if (playerHeroHp <= 0)
        {
            resultText.text = "LOSE";
        }
        else
        {
            resultText.text = "WIN";

        }
    }


}
